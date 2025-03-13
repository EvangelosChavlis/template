// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Municipalities.Mappings;
using server.src.Application.Geography.Administrative.Municipalities.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Municipalities.Dtos;
using server.src.Domain.Geography.Administrative.Municipalities.Models;
using server.src.Domain.Geography.Administrative.Regions.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Administrative.Municipalities.Commands;

public record InitializeMunicipalitiesCommand(List<CreateMunicipalityDto> Dto) : IRequest<Response<string>>;

public class InitializeMunicipalitiesHandler : IRequestHandler<InitializeMunicipalitiesCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InitializeMunicipalitiesHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(InitializeMunicipalitiesCommand command, CancellationToken token = default)
    {
        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        var mununicipalities = new List<Municipality>();
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
            var filters = new Expression<Func<Municipality, bool>>[] 
            { 
                m => m.Name!.Equals(item.Name) ||
                    m.Code!.Equals(item.Code)
            };
            var existingMunicipality = await _commonRepository.GetResultByIdAsync(filters, token: token);

            // Check if the municipality already exists in the system
            if (existingMunicipality is not null)
            {
                failures.Add($"Municipality with name {existingMunicipality.Name} already exists.");
                continue;
            }

            // Searching Item
            var regionFilters = new Expression<Func<Region, bool>>[] { r => r.Id == item.RegionId };
            var region = await _commonRepository.GetResultByIdAsync(regionFilters, token: token);

            // Check for existence
            if (region is null)
            {
                failures.Add("State not found.");
                continue;
            }

            // Mapping and Saving Municipality
            var municipality = item.CreateMunicipalityModelMapping(region);
            var modelValidationResult = municipality.Validate();
            if (!modelValidationResult.IsValid)
            {
                failures.Add(string.Join("\n", modelValidationResult.Errors));
                continue;
            }

            if (existingCodes.Contains(municipality.Code))
            {
                failures.Add($"Code {municipality.Code} already exists in the list of municipalities.");
                continue;
            }

            mununicipalities.Add(municipality);
            existingCodes.Add(municipality.Code);
        }

        var result = await _commonRepository.AddRangeAsync(mununicipalities, token);

        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error initializing municipalities.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(result)
                .WithFailures(failures)
                .WithData("Failed to initialize municipalities.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success initializing municipalities.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithFailures(failures)
            .WithData($"{mununicipalities.Count}/{command.Dto.Count} mununicipalities inserted successfully!");
    }
}
