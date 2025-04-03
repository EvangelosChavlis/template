// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Geography.Natural.SurfaceTypes.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Extensions;
using server.src.Domain.Geography.Natural.SurfaceTypes.Models;
using server.src.Persistence.Common.Interfaces;
using server.src.Domain.Geography.Natural.Locations.Models;

namespace server.src.Application.Geography.Natural.SurfaceTypes.Commands;

public record DeactivateSurfaceTypeCommand(Guid Id, Guid Version) : IRequest<Response<string>>;

public class DeactivateSurfaceTypeHandler : IRequestHandler<DeactivateSurfaceTypeCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly ICommonQueries _commonQueries;
    private readonly IUnitOfWork _unitOfWork;

    public DeactivateSurfaceTypeHandler(ICommonRepository commonRepository,
        ICommonQueries commonQueries, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _commonQueries = commonQueries;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(DeactivateSurfaceTypeCommand command, CancellationToken token = default)
    {
        // Check current user
        var currentUser = await _commonQueries.GetCurrentUser(token);
        if (!currentUser.UserFound)
            return new Response<string>()
                .WithMessage("Error deactivating surface type.")
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
        var surfaceTypeFilters = new Expression<Func<SurfaceType, bool>>[] { s => s.Id == command.Id };
        var surfaceType = await _commonRepository.GetResultByIdAsync(surfaceTypeFilters, token: token);

        // Check for existence
        if (surfaceType is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating surface type.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("SurfaceType not found.");
        }

        // Check for concurrency issues
        if (surfaceType.IsLockedByOtherUser(currentUser.Id))
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity is currently locked.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData(@$"The surface type has been modified by another {surfaceType.LockedByUser!.UserName}. 
                    Please try again.");
        }

        // Check for concurrency issues
        if (surfaceType.Version != command.Version)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Concurrency conflict.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData("The surface type has been modified by another user. Please try again.");
        }

        // Check if the SurfaceType is not active
        if (!surfaceType.IsActive)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating surface type.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData("SurfaceType is deactivated.");
        }

        var locationFilters = new Expression<Func<Location, bool>>[] { l => l.SurfaceTypeId == surfaceType.Id };
        var locationsWithSurfaceType = await _commonRepository.AnyExistsAsync(locationFilters, token);

        if (locationsWithSurfaceType)
        {
            var locationsCounter = await _commonRepository.GetCountAsync(locationFilters, token);

            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating surface type.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData($"This surface type is used from {locationsCounter} and cannot be deactivated.");
        }

        // Validating, Saving Item
        surfaceType.IsActive = false;
        surfaceType.Version = Guid.NewGuid();

        var modelValidationResult = surfaceType.Validate();
        if (!modelValidationResult.IsValid)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(modelValidationResult.IsValid)
                .WithData(string.Join("\n", modelValidationResult.Errors));
        }
        var result = await _commonRepository.UpdateAsync(surfaceType, token);

        // Saving failed
        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating surface type.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to update surface type entity.");
        }

        // Unlock result
        var unlockResult = await _commonRepository.UnlockAsync<SurfaceType>(surfaceType.Id, token);
        if (!unlockResult)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating SurfaceType.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(unlockResult)
                .WithData("Failed to unlock SurfaceType.");
        }

        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Error deactivating SurfaceType.")
            .WithStatusCode((int)HttpStatusCode.Accepted)
            .WithSuccess(result)
            .WithData($"Surface Type {surfaceType.Name} deactivated successfully.");
    }
}