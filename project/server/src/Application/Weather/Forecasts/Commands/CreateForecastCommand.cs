// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Weather.Forecasts.Mappings;
using server.src.Application.Weather.Forecasts.Validators;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Domain.Extensions;
using server.src.Domain.Models.Geography;
using server.src.Domain.Models.Weather;
using server.src.Persistence.Interfaces;

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
        var dtoValidationResult = ForecastValidators.Validate(command.Dto);
        if (!dtoValidationResult.IsValid)
            return new Response<string>()
                .WithMessage("Dto validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(dtoValidationResult.IsValid)
                .WithData(string.Join("\n", dtoValidationResult.Errors));

        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        // Searching Item
        var warningIncludes = new Expression<Func<Warning, object>>[] { };
        var warningFilters = new Expression<Func<Warning, bool>>[] { x => x.Id == command.Dto.WarningId};
        var warning = await _commonRepository.GetResultByIdAsync(warningFilters, warningIncludes, token);

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

        // Searching Item
        var moonPhaseIncludes = new Expression<Func<MoonPhase, object>>[] { };
        var moonPhaseFilters = new Expression<Func<MoonPhase, bool>>[] { x => x.Id == command.Dto.MoonPhaseId};
        var moonPhase = await _commonRepository.GetResultByIdAsync(moonPhaseFilters, moonPhaseIncludes, token);

        // Check for existence
        if(moonPhase is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating forecast.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData($"Moon phase {command.Dto.LocationId} not found");
        }

        // Searching Item
        var locationIncludes = new Expression<Func<Location, object>>[] { };
        var locationFilters = new Expression<Func<Location, bool>>[] { x => x.Id == command.Dto.LocationId};
        var location = await _commonRepository.GetResultByIdAsync(locationFilters, locationIncludes, token);

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

         // Searching Item
        var includes = new Expression<Func<Forecast, object>>[] { f => f.Location };
        var filters = new Expression<Func<Forecast, bool>>[] { 
            f => f.Date.Equals(command.Dto.Date),
            f => f.WarningId == command.Dto.WarningId,
            f => f.MoonPhaseId == command.Dto.MoonPhaseId,
            f => f.LocationId == command.Dto.LocationId
        };
        var existingForecast = await _commonRepository.GetResultByIdAsync(filters, includes, token);
        
        // Check if the forecast already exists in the system
        if(existingForecast is not null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating forecast.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData(@$"Forecast with {existingForecast.Date.GetFullLocalDateTimeString()} 
                    in [({existingForecast.Location.Longitude}),({existingForecast.Location.Latitude})] already exists.");
        }

        // Mapping, Validating, Saving Item
        var forecast = command.Dto.CreateForecastModelMapping(warning, location, moonPhase);
        var modelValidationResult = ForecastValidators.Validate(forecast);
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
            .WithData($"Forecast {forecast.Date.GetLocalDateString()} \nin [({forecast.Location.Longitude}),({forecast.Location.Latitude})] inserted successfully!");
    }
}