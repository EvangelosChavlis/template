// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Geography.Administrative.Neighborhoods.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Extensions;
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;
using server.src.Domain.Geography.Administrative.Stations.Models;
using server.src.Domain.Geography.Natural.Locations.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Administrative.Neighborhoods.Commands;

public record DeactivateNeighborhoodCommand(Guid Id, Guid Version) : IRequest<Response<string>>;

public class DeactivateNeighborhoodHandler : IRequestHandler<DeactivateNeighborhoodCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly ICommonQueries _commonQueries;
    private readonly IUnitOfWork _unitOfWork;
    
    public DeactivateNeighborhoodHandler(ICommonRepository commonRepository, 
        ICommonQueries commonQueries, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _commonQueries = commonQueries;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(DeactivateNeighborhoodCommand command, CancellationToken token = default)
    {
        // Check current user
        var currentUser = await _commonQueries.GetCurrentUser(token);
        if(!currentUser.UserFound)
            return new Response<string>()
                .WithMessage("Error deactivating neighborhood.")
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
        var neighborhoodFilters = new Expression<Func<Neighborhood, bool>>[] { n => n.Id == command.Id};
        var neighborhood = await _commonRepository.GetResultByIdAsync(neighborhoodFilters, token: token);

        // Check for existence
        if (neighborhood is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating neighborhood.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Neighborhood not found.");
        }

        // Check for concurrency issues
        if (neighborhood.IsLockedByOtherUser(currentUser.Id))
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity is currently locked.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData(@$"The neighborhood has been modified by another {neighborhood.LockedByUser!.UserName}. 
                    Please try again.");
        }

        // Check for concurrency issues
        if (neighborhood.Version != command.Version)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Concurrency conflict.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData("The neighborhood has been modified by another user. Please try again.");
        }
        
        // Check if the Neighborhood is not active
        if (!neighborhood.IsActive)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating .")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData("Neighborhood is deactivated.");
        }
            
        var stationFilters = new Expression<Func<Station, bool>>[] { l =>l.NeighborhoodId == neighborhood.Id };
        var stationWithNeighborhood = await _commonRepository.AnyExistsAsync(stationFilters, token);
        
        if (stationWithNeighborhood)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating neighborhood.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData($"This neighborhood is used and cannot be deactivated.");
        }
            
        // Validating, Saving Item
        neighborhood.IsActive = false;
        neighborhood.Version = Guid.NewGuid();

        var modelValidationResult = neighborhood.Validate();
        if (!modelValidationResult.IsValid)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(modelValidationResult.IsValid)
                .WithData(string.Join("\n", modelValidationResult.Errors));
        }
        var result = await _commonRepository.UpdateAsync(neighborhood, token);

        // Saving failed
        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating neighborhood.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to update neighborhood entity.");
        }

        // Unlock result
        var unlockResult = await _commonRepository.UnlockAsync<Neighborhood>(neighborhood.Id, token);
        if(!unlockResult)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deactivating neighborhood.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(unlockResult)
                .WithData("Failed to unlock neighborhood.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);
    
        // Initializing object
        return new Response<string>()
            .WithMessage("Error deactivating neighborhood.")
            .WithStatusCode((int)HttpStatusCode.Accepted)
            .WithSuccess(result)
            .WithData($"Neighborhood {neighborhood.Name} deactivated successfully.");
    }
}