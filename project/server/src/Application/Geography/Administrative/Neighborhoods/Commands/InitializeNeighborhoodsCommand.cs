// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Neighborhoods.Mappings;
using server.src.Application.Geography.Administrative.Neighborhoods.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Districts.Models;
using server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Administrative.Neighborhoods.Commands;

public record InitializeNeighborhoodsCommand(List<CreateNeighborhoodDto> Dto) : IRequest<Response<string>>;

public class InitializeNeighborhoodsHandler : IRequestHandler<InitializeNeighborhoodsCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InitializeNeighborhoodsHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(InitializeNeighborhoodsCommand command, CancellationToken token = default)
    {
        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        var neighborhoods = new List<Neighborhood>();
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
            var filters = new Expression<Func<Neighborhood, bool>>[] 
            { 
                n => n.Name!.Equals(item.Name) ||
                    n.Code!.Equals(item.Code)
            };
            var existingNeighborhood = await _commonRepository.GetResultByIdAsync(filters, token: token);

            // Check if the neighborhood already exists in the system
            if (existingNeighborhood is not null)
            {
                failures.Add($"Neighborhood with name {existingNeighborhood.Name} already exists.");
                continue;
            }

            // Searching Item
            var districtFilters = new Expression<Func<District, bool>>[] 
            { 
                d => d.Id == item.DistrictId 
            };
            var district = await _commonRepository.GetResultByIdAsync(districtFilters, token: token);

            // Check for existence
            if (district is null)
            {
                failures.Add("District not found.");
                continue;
            }

            // Mapping and Saving Neighborhood
            var neighborhood = item.CreateNeighborhoodModelMapping(district);
            var modelValidationResult = neighborhood.Validate();
            if (!modelValidationResult.IsValid)
            {
                failures.Add(string.Join("\n", modelValidationResult.Errors));
                continue;
            }

            if (existingCodes.Contains(neighborhood.Code))
            {
                failures.Add($"Code {neighborhood.Code} already exists in the list of neighborhoods.");
                continue;
            }

            neighborhoods.Add(neighborhood);
            existingCodes.Add(neighborhood.Code);
        }

        var result = await _commonRepository.AddRangeAsync(neighborhoods, token);

        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error initializing neighborhood.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(result)
                .WithFailures(failures)
                .WithData("Failed to initialize neighborhood.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success initializing neighborhoods.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithFailures(failures)
            .WithData($"{neighborhoods.Count}/{command.Dto.Count} neighborhoods inserted successfully!");
    }
}
