// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Weather.Collections.Observations.Mappings;
using server.src.Application.Weather.Collections.Observations.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Weather.Collections.Observations.Dtos;
using server.src.Domain.Weather.Collections.Observations.Models;
using server.src.Domain.Common.Extensions;
using server.src.Persistence.Common.Interfaces;
using server.src.Domain.Geography.Natural.Locations.Models;
using server.src.Domain.Weather.Collections.MoonPhases.Models;
using server.src.Domain.Geography.Administrative.Stations.Models;

namespace server.src.Application.Weather.Collections.Observations.Commands;

public record UpdateObservationCommand(Guid Id, UpdateObservationDto Dto) : IRequest<Response<string>>;

public class UpdateObservationHandler : IRequestHandler<UpdateObservationCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly ICommonQueries _commonQueries;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateObservationHandler(ICommonRepository commonRepository, 
        ICommonQueries commonQueries, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _commonQueries = commonQueries;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(UpdateObservationCommand command, CancellationToken token = default)
    {
        // Check current user
        var currentUser = await _commonQueries.GetCurrentUser(token);
        if(!currentUser.UserFound)
            return new Response<string>()
                .WithMessage("Error updating observation.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(currentUser.UserFound)
                .WithData("Current user not found");

        // Validation
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
        var observationFilters = new Expression<Func<Observation, bool>>[] { x => x.Id == command.Id};
        var observation = await _commonRepository.GetResultByIdAsync(observationFilters, token: token);

        // Check for existence
        if (observation is null)
        {
            return new Response<string>()
                .WithMessage("Error updating observation.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Observation not found");
        }

        // Check for concurrency issues
        if (observation.IsLockedByOtherUser(currentUser.Id))
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity is currently locked.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData(@$"The observation has been modified by another {observation.LockedByUser!.UserName}. 
                    Please try again.");
        }
            
        // Check for concurrency issues
        if (observation.Version != command.Dto.Version)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Concurrency conflict.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData("The role has been modified by another user. Please try again.");
        }

        // Searching Item
        var stationFilters = new Expression<Func<Station, bool>>[] { l => l.Id == command.Dto.StationId};
        var station = await _commonRepository.GetResultByIdAsync(stationFilters, token: token);
        
        // Check for existence
        if (station is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating observation.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Station not found");
        }

        // Searching Item
        var moonPhaseFilters = new Expression<Func<MoonPhase, bool>>[] { l => l.Id == command.Dto.MoonPhaseId};
        var moonPhase = await _commonRepository.GetResultByIdAsync(moonPhaseFilters, token: token);
        
        // Check for existence
        if (moonPhase is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating observation.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Moon phase not found");
        }

        // Mapping, Validating, Saving Item
        command.Dto.UpdateObservationModelMapping(observation, 
            station, moonPhase);
        var modelValidationResult = ObservationModelValidators.Validate(observation);
        if (!modelValidationResult.IsValid)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(modelValidationResult.IsValid)
                .WithData(string.Join("\n", modelValidationResult.Errors));
        }
        var result = await _commonRepository.UpdateAsync(observation, token);

        // Saving failed.
        if(!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating observation.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to update observation.");
        }

        // Unlock result
        var unlockResult = await _commonRepository.UnlockAsync<Observation>(observation.Id, token);
        if(!unlockResult)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating observation.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(unlockResult)
                .WithData("Failed to update observation.");
        }

        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);
            
        // Initializing object
        return new Response<string>()
            .WithMessage("Success updating observation.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithData($"Observation {observation.Timestamp.GetLocalDateString()} updated successfully!");
    }
}