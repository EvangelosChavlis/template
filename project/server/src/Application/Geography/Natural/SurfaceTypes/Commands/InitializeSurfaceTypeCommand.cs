// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Natural.SurfaceTypes.Mappings;
using server.src.Application.Geography.Natural.SurfaceTypes.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.SurfaceTypes.Dtos;
using server.src.Domain.Geography.Natural.SurfaceTypes.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Natural.SurfaceTypes.Commands;

public record InitializeSurfaceTypesCommand(List<CreateSurfaceTypeDto> Dto) : IRequest<Response<string>>;

public class InitializeSurfaceTypesHandler : IRequestHandler<InitializeSurfaceTypesCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InitializeSurfaceTypesHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(InitializeSurfaceTypesCommand command, CancellationToken token = default)
    {
        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        var surfaceTypes = new List<SurfaceType>();
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
            var filters = new Expression<Func<SurfaceType, bool>>[] 
            { 
                s => s.Name!.Equals(item.Name) ||
                    s.Code!.Equals(item.Code)  
            };
            var existingSurfaceType = await _commonRepository.GetResultByIdAsync(filters, token: token);

            // Check if the surface type already exists in the system
            if (existingSurfaceType is not null)
            {
                failures.Add($"Surface Type with name {existingSurfaceType.Name} or with code {existingSurfaceType.Code} already exists.");
                continue;
            }

            // Mapping and Saving SurfaceType
            var surfaceType = item.CreateSurfaceTypeModelMapping();
            var modelValidationResult = surfaceType.Validate();
            if (!modelValidationResult.IsValid)
            {
                failures.Add(string.Join("\n", modelValidationResult.Errors));
                continue;
            }
        
            if (existingCodes.Contains(surfaceType.Code))
            {
                failures.Add($"Code {surfaceType.Code} already exists in the list of surfaceTypes.");
                continue;
            }
            
            surfaceTypes.Add(surfaceType);
            existingCodes.Add(surfaceType.Code);
        }

        var result = await _commonRepository.AddRangeAsync(surfaceTypes, token);

        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error initializing surface types.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(result)
                .WithFailures(failures)
                .WithData("Failed to initialize surface types.");
        }

        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success initializing surface types.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithFailures(failures)
            .WithData($"{surfaceTypes.Count}/{command.Dto.Count} surface types inserted successfully!");
    }
}
