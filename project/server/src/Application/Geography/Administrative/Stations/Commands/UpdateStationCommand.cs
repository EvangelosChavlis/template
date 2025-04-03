// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Geography.Administrative.Stations.Mappings;
using server.src.Application.Geography.Administrative.Stations.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Stations.Dtos;
using server.src.Domain.Geography.Administrative.Stations.Models;
using server.src.Domain.Common.Extensions;
using server.src.Persistence.Common.Interfaces;
using server.src.Domain.Geography.Natural.Locations.Models;
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;

namespace server.src.Application.Geography.Administrative.Stations.Commands;

public record UpdateStationCommand(Guid Id, UpdateStationDto Dto) : IRequest<Response<string>>;

public class UpdateStationHandler : IRequestHandler<UpdateStationCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly ICommonQueries _commonQueries;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateStationHandler(ICommonRepository commonRepository, 
        ICommonQueries commonQueries, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _commonQueries = commonQueries;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(UpdateStationCommand command, CancellationToken token = default)
    {
        // Check current user
        var currentUser = await _commonQueries.GetCurrentUser(token);
        if(!currentUser.UserFound)
            return new Response<string>()
                .WithMessage("Error updating station.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(currentUser.UserFound)
                .WithData("User not found");

        // Id Validation
        var idValidationResult = command.Id.ValidateId();
        if (!idValidationResult.IsValid)
            return new Response<string>()
                .WithMessage("Validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(idValidationResult.IsValid)
                .WithData(string.Join("\n", idValidationResult.Errors));

        // Dto Validation
        var validationResult = command.Dto.Validate();
        if (!validationResult.IsValid)
            return new Response<string>()
                .WithMessage("Validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(validationResult.IsValid)
                .WithData(string.Join("\n", validationResult.Errors));

        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        // Searching Item
        var filters = new Expression<Func<Station, bool>>[] { s => s.Id == command.Id};
        var station = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check for existence
        if (station is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating station.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Station not found");
        }

        // Check for concurrency issues
        if (station.IsLockedByOtherUser(currentUser.Id))
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity is currently locked.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData(@$"The station has been modified by another {station.LockedByUser!.UserName}. 
                    Please try again.");
        }

        // Check for concurrency issues
        if (station.Version != command.Dto.Version)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Concurrency conflict.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData("The station has been modified by another user. Please try again.");
        }

        // Searching Item
        var locationFilters = new Expression<Func<Location, bool>>[] { l => l.Id == command.Dto.LocationId };
        var location = await _commonRepository.GetResultByIdAsync(locationFilters, token: token);

        // Check for existence
        if (location is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating station.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Location not found.");
        }

        // Searching Item
        var neighborhoodFilters = new Expression<Func<Neighborhood, bool>>[] { n => n.Id == command.Dto.NeighborhoodId };
        var neighborhood = await _commonRepository.GetResultByIdAsync(neighborhoodFilters, token: token);

        // Mapping, Validating, Saving Item
        command.Dto.UpdateStationMapping(station, location, neighborhood);
        var modelValidationResult = station.Validate();
        if (!modelValidationResult.IsValid)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(modelValidationResult.IsValid)
                .WithData(string.Join("\n", modelValidationResult.Errors));
        }
        var result = await _commonRepository.UpdateAsync(station, token);

        // Saving failed.
        if(!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating station.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to update station.");
        }

        // Unlock result
        var unlockResult = await _commonRepository.UnlockAsync<Station>(station.Id, token);
        if(!unlockResult)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating station.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(unlockResult)
                .WithData("Failed to update station.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success updating station.")
            .WithStatusCode((int)HttpStatusCode.Created)
            .WithSuccess(result)
            .WithData($"Station {station.Name} updated successfully!");
    }
}