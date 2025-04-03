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
using server.src.Domain.Geography.Natural.Locations.Models;

namespace server.src.Application.Geography.Natural.NaturalFeatures.Commands;

public record DeactivateNaturalFeatureCommand(Guid Id, Guid Version) : IRequest<Response<string>>;

public class DeactivateNaturalFeatureHandler : IRequestHandler<DeactivateNaturalFeatureCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly ICommonQueries _commonQueries;
    private readonly IUnitOfWork _unitOfWork;
    
    public DeactivateNaturalFeatureHandler(ICommonRepository commonRepository, 
        ICommonQueries commonQueries, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _commonQueries = commonQueries;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(DeactivateNaturalFeatureCommand command, CancellationToken token = default)
    {
        // Check current user
        var currentUser = await _commonQueries.GetCurrentUser(token);
        if(!currentUser.UserFound)
            return new Response<string>()
                .WithMessage("Error deactivating natural feature.")
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

        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        // Searching Item
        var NaturalFeatureFilters = new Expression<Func<NaturalFeature, bool>>[] { t => t.Id == command.Id};
        var NaturalFeature = await _commonRepository.GetResultByIdAsync(NaturalFeatureFilters, token: token);

        // Check for existence
        if (NaturalFeature is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating natural feature.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("NaturalFeature not found.");
        }

        // Check for concurrency issues
        if (NaturalFeature.IsLockedByOtherUser(currentUser.Id))
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity is currently locked.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData(@$"The natural feature has been modified by another {NaturalFeature.LockedByUser!.UserName}. 
                    Please try again.");
        }

        // Check for concurrency issues
        if (NaturalFeature.Version != command.Version)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Concurrency conflict.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData("The natural feature has been modified by another user. Please try again.");
        }
        
        // Check if the NaturalFeature is not active
        if (!NaturalFeature.IsActive)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating natural feature.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData("Natural feature is deactivated.");
        }
            
        var locationFilters = new Expression<Func<Location, bool>>[] { l => l.NaturalFeatureId == NaturalFeature.Id };
        var locationsWithNaturalFeature = await _commonRepository.AnyExistsAsync(locationFilters, token);
        
        if (locationsWithNaturalFeature)
        {
            var locationsCounter = await _commonRepository.GetCountAsync(locationFilters, token);

            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating natural feature.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData($"This natural feature is used from {locationsCounter} and cannot be deactivated.");
        }
            
        // Validating, Saving Item
        NaturalFeature.IsActive = false;
        NaturalFeature.Version = Guid.NewGuid();

        var modelValidationResult = NaturalFeature.Validate();
        if (!modelValidationResult.IsValid)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(modelValidationResult.IsValid)
                .WithData(string.Join("\n", modelValidationResult.Errors));
        }
        var result = await _commonRepository.UpdateAsync(NaturalFeature, token);

        // Saving failed
        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating natural feature.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to update natural feature entity.");
        }

        // Unlock result
        var unlockResult = await _commonRepository.UnlockAsync<NaturalFeature>(NaturalFeature.Id, token);
        if(!unlockResult)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating natural feature.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(unlockResult)
                .WithData("Failed to unlock natural feature.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);
    
        // Initializing object
        return new Response<string>()
            .WithMessage("Success deactivating natural feature.")
            .WithStatusCode((int)HttpStatusCode.Accepted)
            .WithSuccess(result)
            .WithData($"Natural feature {NaturalFeature.Name} deactivated successfully.");
    }
}