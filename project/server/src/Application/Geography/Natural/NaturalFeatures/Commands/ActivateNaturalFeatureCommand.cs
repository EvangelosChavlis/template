// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Geography.Natural.NaturalFeatures.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Extensions;
using server.src.Domain.Geography.Natural.NaturalFeatures.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Natural.NaturalFeatures.Commands;

public record ActivateNaturalFeatureCommand(Guid Id, Guid Version) : IRequest<Response<string>>;

public class ActivateNaturalFeatureHandler : IRequestHandler<ActivateNaturalFeatureCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly ICommonQueries _commonQueries;
    private readonly IUnitOfWork _unitOfWork;
    
    public ActivateNaturalFeatureHandler(ICommonRepository commonRepository, 
        ICommonQueries commonQueries, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _commonQueries = commonQueries;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(ActivateNaturalFeatureCommand command, CancellationToken token = default)
    {
        // Check current user
        var currentUser = await _commonQueries.GetCurrentUser(token);
        if(!currentUser.UserFound)
            return new Response<string>()
                .WithMessage("Error activating natural feature.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(currentUser.UserFound)
                .WithData("Current user not found");

        // Id Validation
        var idValidationResult = command.Id.ValidateId();
        if (!idValidationResult.IsValid)
            return new Response<string>()
                .WithMessage("Validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(idValidationResult.IsValid)
                .WithData(string.Join("\n", idValidationResult.Errors));

        // Version Validation
        var versionValidationResult = command.Version.ValidateId();
        if (!versionValidationResult.IsValid)
            return new Response<string>()
                .WithMessage("Validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(versionValidationResult.IsValid)
                .WithData(string.Join("\n", versionValidationResult.Errors));

        // Begin transaction
        await _unitOfWork.BeginTransactionAsync(token);

        // Searching Item
        var naturalFeatureFilters = new Expression<Func<NaturalFeature, bool>>[] { nf => nf.Id == command.Id};
        var naturalFeature = await _commonRepository.GetResultByIdAsync(naturalFeatureFilters, token: token);

        // Check for existence
        if (naturalFeature is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating natural feature.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("NaturalFeature not found.");
        }

        // Check for concurrency issues
        if (naturalFeature.IsLockedByOtherUser(currentUser.Id))
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity is currently locked.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData(@$"The natural feature has been modified by another {naturalFeature.LockedByUser!.UserName}. 
                    Please try again.");
        }

        // Check for concurrency issues
        if (naturalFeature.Version != command.Version)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error activating natural feature.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData("The natural feature was updated by another user. Please reload and try again.");
        }
            
        // Check if the natural feature is already active
        if (naturalFeature.IsActive)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error activating natural feature.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData("Natural feature is activated.");
        }

        // Validating, Saving Item
        naturalFeature.IsActive = true;
        naturalFeature.Version = Guid.NewGuid();
        
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
        var result = await _commonRepository.UpdateAsync(naturalFeature, token);

        // Saving failed
        if(!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error activating natural feature.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to update natural feature entity.");
        }

        // Unlock result
        var unlockResult = await _commonRepository.UnlockAsync<NaturalFeature>(naturalFeature.Id, token);
        if(!unlockResult)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error activating natural feature.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(unlockResult)
                .WithData("Failed to unlock natural feature.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success activating natural feature.")
            .WithStatusCode((int)HttpStatusCode.Accepted)
            .WithSuccess(result)
            .WithData($"Natural feature {naturalFeature.Name} activated successfully.");
    }
} 