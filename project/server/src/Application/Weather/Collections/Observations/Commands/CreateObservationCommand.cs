// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Weather.Collections.Observations.Mappings;
using server.src.Application.Weather.Collections.Observations.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Extensions;
using server.src.Domain.Weather.Collections.Observations.Dtos;
using server.src.Domain.Weather.Collections.Observations.Models;
using server.src.Domain.Weather.Collections.MoonPhases.Models;
using server.src.Persistence.Common.Interfaces;
using server.src.Domain.Geography.Administrative.Stations.Models;

namespace server.src.Application.Weather.Collections.Observations.Commands;

public record CreateObservationCommand(CreateObservationDto Dto) : IRequest<Response<string>>;

public class CreateObservationHandler : IRequestHandler<CreateObservationCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateObservationHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(CreateObservationCommand command, CancellationToken token = default)
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
        var includes = new Expression<Func<Observation, object>>[] { o => o.Station };
        var filters = new Expression<Func<Observation, bool>>[] { 
            o => o.Timestamp.Equals(command.Dto.Timestamp),
            o => o.MoonPhaseId == command.Dto.MoonPhaseId,
            o => o.StationId == command.Dto.StationId
        };
        var existingObservation = await _commonRepository.GetResultByIdAsync(filters, includes, token: token);
        
        // Check if the observation already exists in the system
        if(existingObservation is not null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating observation.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData($"Observation with {existingObservation.Timestamp.GetFullLocalDateTimeString()} already exists.");
        }

        // Searching Item
        var moonPhaseFilters = new Expression<Func<MoonPhase, bool>>[] { m => m.Id == command.Dto.MoonPhaseId};
        var moonPhase = await _commonRepository.GetResultByIdAsync(moonPhaseFilters, token: token);

        // Check for existence
        if(moonPhase is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating observation.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData($"Moon phase {command.Dto.MoonPhaseId} not found");
        }

        // Check for inactivity
        if(!moonPhase.IsActive)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating observation.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData($"Moon phase {moonPhase.Name} is inactive");
        }

        // Searching Item
        var stationFilters = new Expression<Func<Station, bool>>[] { s => s.Id == command.Dto.StationId};
        var station = await _commonRepository.GetResultByIdAsync(stationFilters, token: token);

        // Check for existence
        if(station is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating observation.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData($"Station {command.Dto.StationId} not found");
        }

        // Check for inactivity
        if(!station.IsActive)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating observation.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData($"Station ({station.Name} - {station.Code}) is inactive");
        }

        // Mapping, Validating, Saving Item
        var observation = command.Dto.CreateObservationModelMapping(station, moonPhase);
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
        var result = await _commonRepository.AddAsync(observation, token);

        // Saving failed
        if(!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating observation.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to create observation.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success creating observation.")
            .WithStatusCode((int)HttpStatusCode.Created)
            .WithSuccess(result)
            .WithData($"Observation {observation.Timestamp.GetLocalDateString()} inserted successfully!");

    }
}