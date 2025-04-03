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

public record CreateNaturalFeatureCommand(CreateNaturalFeatureDto Dto) : IRequest<Response<string>>;

public class CreateNaturalFeatureHandler : IRequestHandler<CreateNaturalFeatureCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateNaturalFeatureHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(CreateNaturalFeatureCommand command, CancellationToken token = default)
    {
        // Dto Validation
        var dtoValidationResult = command.Dto.Validate();
        if (!dtoValidationResult.IsValid)
            return new Response<string>()
                .WithMessage("Dto validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(dtoValidationResult.IsValid)
                .WithData(string.Join("\n", dtoValidationResult.Errors));

        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        // Searching Item
        var filters = new Expression<Func<NaturalFeature, bool>>[] 
        { 
            s => s.Name!.Equals(command.Dto.Name) ||
                s.Code!.Equals(command.Dto.Code)  
        };
        var existingNaturalFeature = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check if the natural feature already exists in the system
        if (existingNaturalFeature is not null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating NaturalFeature.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData($"Natural feature with name {existingNaturalFeature.Name} or with code {existingNaturalFeature.Code} already exists.");
        }

        // Mapping and Saving NaturalFeature
        var naturalFeature = command.Dto.CreateNaturalFeatureModelMapping();
        var modelValidationResult = naturalFeature.Validate();
        if (!modelValidationResult.IsValid)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(modelValidationResult.IsValid)
                .WithData(string.Join("\n", modelValidationResult.Errors));
        }
        var result = await _commonRepository.AddAsync(naturalFeature, token);

        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating natural feature.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to create natural feature.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success creating natural feature.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithData($"Natural feature {naturalFeature.Name} inserted successfully!");
    }
}