// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Weather.Forecasts.Mappings;
using server.src.Application.Weather.Forecasts.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Extensions;
using server.src.Domain.Geography.Natural.Locations.Models;
using server.src.Domain.Weather.Forecasts.Dtos;
using server.src.Domain.Weather.Forecasts.Models;
using server.src.Domain.Weather.MoonPhases.Models;
using server.src.Domain.Weather.Warnings.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Weather.Forecasts.Commands;

public record CreateForecastCommand(CreateForecastDto Dto) : IRequest<Response<string>>;

public class CreateForecastHandler : IRequestHandler<CreateForecastCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateForecastHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(CreateForecastCommand command, CancellationToken token = default)
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
        var includes = new Expression<Func<Forecast, object>>[] { f => f.Location };
        var filters = new Expression<Func<Forecast, bool>>[] { 
            f => f.Date.Equals(command.Dto.Date),
            f => f.WarningId == command.Dto.WarningId,
            f => f.MoonPhaseId == command.Dto.MoonPhaseId,
            f => f.LocationId == command.Dto.LocationId
        };
        var existingForecast = await _commonRepository.GetResultByIdAsync(filters, includes, token: token);
        
        // Check if the forecast already exists in the system
        if(existingForecast is not null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating forecast.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData(@$"Forecast with {existingForecast.Date.GetFullLocalDateTimeString()} 
                    in ({existingForecast.Location.Latitude}, {existingForecast.Location.Longitude}, {existingForecast.Location.Altitude}) 
                    already exists.");
        }

        // Searching Item
        var warningFilters = new Expression<Func<Warning, bool>>[] { w => w.Id == command.Dto.WarningId};
        var warning = await _commonRepository.GetResultByIdAsync(warningFilters, token: token);

        // Check for existence
        if(warning is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating forecast.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData($"Warning {command.Dto.WarningId} not found");
        }

        // Check for inactivity
        if(!warning.IsActive)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating forecast.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData($"Warning {warning.Name} is inactive");
        }

        // Searching Item
        var moonPhaseFilters = new Expression<Func<MoonPhase, bool>>[] { x => x.Id == command.Dto.MoonPhaseId};
        var moonPhase = await _commonRepository.GetResultByIdAsync(moonPhaseFilters, token: token);

        // Check for existence
        if(moonPhase is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating forecast.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData($"Moon phase {command.Dto.MoonPhaseId} not found");
        }

        // Check for inactivity
        if(!moonPhase.IsActive)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating forecast.")
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
                .WithMessage("Error creating forecast.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData($"Location {command.Dto.LocationId} not found");
        }

        // Check for inactivity
        if(!location.IsActive)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating forecast.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData($"Location ({location.Latitude}, {location.Longitude}, {location.Altitude}) is inactive");
        }

        // Mapping, Validating, Saving Item
        var forecast = command.Dto.CreateForecastModelMapping(warning, 
            location, moonPhase);
        var modelValidationResult = forecast.Validate();
        if (!modelValidationResult.IsValid)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(modelValidationResult.IsValid)
                .WithData(string.Join("\n", modelValidationResult.Errors));
        }
        var result = await _commonRepository.AddAsync(forecast, token);

        // Saving failed
        if(!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating forecast.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to create forecast.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success creating forecast.")
            .WithStatusCode((int)HttpStatusCode.Created)
            .WithSuccess(result)
            .WithData($"Forecast {forecast.Date.GetLocalDateString()} \n"
                + @$"in ({forecast.Location.Latitude}, {forecast.Location.Longitude}, {forecast.Location.Altitude}) 
                inserted successfully!");

    }
}