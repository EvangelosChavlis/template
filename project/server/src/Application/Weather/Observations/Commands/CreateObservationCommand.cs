// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Weather.Observations.Mappings;
using server.src.Application.Weather.Observations.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Extensions;
using server.src.Domain.Weather.Observations.Dtos;
using server.src.Domain.Weather.Observations.Models;
using server.src.Domain.Weather.MoonPhases.Models;
using server.src.Persistence.Common.Interfaces;
using server.src.Domain.Geography.Natural.Locations.Models;

namespace server.src.Application.Weather.Observations.Commands;

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
        var includes = new Expression<Func<Observation, object>>[] { f => f.Location };
        var filters = new Expression<Func<Observation, bool>>[] { 
            f => f.Timestamp.Equals(command.Dto.Timestamp),
            f => f.MoonPhaseId == command.Dto.MoonPhaseId,
            f => f.LocationId == command.Dto.LocationId
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
                .WithData(@$"Observation with {existingObservation.Timestamp.GetFullLocalDateTimeString()} 
                    in ({existingObservation.Location.Latitude}, {existingObservation.Location.Longitude}, {existingObservation.Location.Altitude}) 
                    already exists.");
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
        var locationFilters = new Expression<Func<Location, bool>>[] { x => x.Id == command.Dto.LocationId};
        var location = await _commonRepository.GetResultByIdAsync(locationFilters, token: token);

        // Check for existence
        if(location is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating observation.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData($"Location {command.Dto.LocationId} not found");
        }

        // Check for inactivity
        if(!location.IsActive)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating observation.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData($"Location ({location.Latitude}, {location.Longitude}, {location.Altitude}) is inactive");
        }

        // Mapping, Validating, Saving Item
        var observation = command.Dto.CreateObservationModelMapping(location, moonPhase);
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
            .WithData($"Observation {observation.Timestamp.GetLocalDateString()} \n"
                + @$"in ({observation.Location.Latitude}, {observation.Location.Longitude}, {observation.Location.Altitude}) 
                inserted successfully!");

    }
}