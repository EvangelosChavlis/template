// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Districts.Mappings;
using server.src.Application.Geography.Administrative.Districts.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Districts.Dtos;
using server.src.Domain.Geography.Administrative.Districts.Models;
using server.src.Domain.Geography.Administrative.Municipalities.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Administrative.Districts.Commands;

public record InitializeDistrictsCommand(List<CreateDistrictDto> Dto) : IRequest<Response<string>>;

public class InitializeDistrictsHandler : IRequestHandler<InitializeDistrictsCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InitializeDistrictsHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(InitializeDistrictsCommand command, CancellationToken token = default)
    {
        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        var districts = new List<District>();
        var existingCodes = new HashSet<string>();

        var failures = new List<string>();
        
        foreach (var item in command.Dto)
        {
            // Dto Validation
            var dtoValidationResult = item.Validate();
            if (!dtoValidationResult.IsValid)
            {
                failures.Add(string.Join("\n", dtoValidationResult.Errors));
                continue;
            }

            // Searching Item
            var filters = new Expression<Func<District, bool>>[] 
            { 
                d => d.Name!.Equals(item.Name) ||
                    d.Code!.Equals(item.Code)
            };
            var existingDistrict = await _commonRepository.GetResultByIdAsync(filters, token: token);

            // Check if the district already exists in the system
            if (existingDistrict is not null)
            {
                failures.Add($"District with name {existingDistrict.Name} already exists.");
                continue;
            }

            // Searching Item
            var municipalityFilters = new Expression<Func<Municipality, bool>>[] 
            { 
                m => m.Id == item.MunicipalityId 
            };
            var municipality = await _commonRepository.GetResultByIdAsync(municipalityFilters, token: token);

            // Check for existence
            if (municipality is null)
            {
                failures.Add("Municipality not found.");
                continue;
            }

            // Mapping and Saving District
            var district = item.CreateDistrictModelMapping(municipality);
            var modelValidationResult = district.Validate();
            if (!modelValidationResult.IsValid)
            {
                failures.Add(string.Join("\n", modelValidationResult.Errors));
                continue;
            }

            if (existingCodes.Contains(district.Code))
            {
                failures.Add($"Code {district.Code} already exists in the list of districts.");
                continue;
            }

            districts.Add(district);
            existingCodes.Add(district.Code);
        }

        var result = await _commonRepository.AddRangeAsync(districts, token);

        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error initializing districts.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(result)
                .WithFailures(failures)
                .WithData("Failed to initialize districts.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success initializing districts.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithFailures(failures)
            .WithData($"{districts.Count}/{command.Dto.Count} districts inserted successfully!");
    }
}
