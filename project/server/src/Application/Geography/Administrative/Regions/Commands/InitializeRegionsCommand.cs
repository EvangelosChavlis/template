// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Regions.Mappings;
using server.src.Application.Geography.Administrative.Regions.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Regions.Dtos;
using server.src.Domain.Geography.Administrative.Regions.Models;
using server.src.Domain.Geography.Administrative.States.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Administrative.Regions.Commands;

public record InitializeRegionsCommand(List<CreateRegionDto> Dto) : IRequest<Response<string>>;


public class InitializeRegionsHandler : IRequestHandler<InitializeRegionsCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InitializeRegionsHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(InitializeRegionsCommand command, CancellationToken token = default)
    {
        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        var regions = new List<Region>();
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
            var stateFilters = new Expression<Func<State, bool>>[] { s => s.Id == item.StateId };
            var state = await _commonRepository.GetResultByIdAsync(stateFilters, token: token);

            // Check for existence
            if (state is null)
            {
                failures.Add("State not found.");
                continue;
            }

            // Mapping and Saving Region
            var region = item.CreateRegionModelMapping(state);
            var modelValidationResult = region.Validate();
            if (!modelValidationResult.IsValid)
            {
                failures.Add(string.Join("\n", modelValidationResult.Errors));
                continue;
            }

            if (existingCodes.Contains(region.Code))
            {
                failures.Add($"Code {region.Code} already exists in the list of regions.");
                continue;
            }

            regions.Add(region);
            existingCodes.Add(region.Code);
        }

        var result = await _commonRepository.AddRangeAsync(regions, token);

        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error initializing region.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(result)
                .WithFailures(failures)
                .WithData("Failed to initialize region.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success initializing regions.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithFailures(failures)
            .WithData($"{regions.Count}/{command.Dto.Count} regions inserted successfully!");
    }
}
