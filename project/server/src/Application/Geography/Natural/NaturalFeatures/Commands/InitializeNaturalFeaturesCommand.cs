// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Natural.NaturalFeatures.Mappings;
using server.src.Application.Geography.Natural.NaturalFeatures.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.NaturalFeatures.Dtos;
using server.src.Domain.Geography.Natural.NaturalFeatures.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Natural.NaturalFeatures.Commands;

public record InitializeNaturalFeaturesCommand(List<CreateNaturalFeatureDto> Dto) : IRequest<Response<string>>;

public class InitializeNaturalFeaturesHandler : IRequestHandler<InitializeNaturalFeaturesCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InitializeNaturalFeaturesHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(InitializeNaturalFeaturesCommand command, CancellationToken token = default)
    {
        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        var naturalFeatures = new List<NaturalFeature>();
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
            var filters = new Expression<Func<NaturalFeature, bool>>[] 
            { 
                s => s.Name!.Equals(item.Name) ||
                    s.Code!.Equals(item.Code)  
            };
            var existingNaturalFeature = await _commonRepository.GetResultByIdAsync(filters, token: token);

            // Check if the natural feature already exists in the system
            if (existingNaturalFeature is not null)
            {
                failures.Add($"Natural feature with name {existingNaturalFeature.Name} or with code {existingNaturalFeature.Code} already exists.");
                continue;
            }

            // Mapping and Saving NaturalFeature
            var naturalFeature = item.CreateNaturalFeatureModelMapping();
            var modelValidationResult = naturalFeature.Validate();
            if (!modelValidationResult.IsValid)
            {
                failures.Add(string.Join("\n", modelValidationResult.Errors));
                continue;
            }
        
            if (existingCodes.Contains(naturalFeature.Code))
            {
                failures.Add($"Code {naturalFeature.Code} already exists in the list of naturalFeatures.");
                continue;
            }
            
            naturalFeatures.Add(naturalFeature);
            existingCodes.Add(naturalFeature.Code);
        }

        var result = await _commonRepository.AddRangeAsync(naturalFeatures, token);

        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error initializing natural features.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(result)
                .WithFailures(failures)
                .WithData("Failed to initialize natural features.");
        }

        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success initializing natural features.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithFailures(failures)
            .WithData($"{naturalFeatures.Count}/{command.Dto.Count} natural features inserted successfully!");
    }
}
