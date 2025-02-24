// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Weather.Forecasts.Mappings;
using server.src.Application.Weather.Forecasts.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Weather.Forecasts.Dtos;
using server.src.Domain.Weather.Forecasts.Models;
using server.src.Domain.Common.Extensions;
using server.src.Persistence.Common.Interfaces;
using server.src.Domain.Weather.Warnings.Models;
using server.src.Domain.Weather.MoonPhases.Models;
using server.src.Domain.Geography.Natural.Locations.Models;

namespace server.src.Application.Weather.Forecasts.Commands;

public record UpdateForecastCommand(Guid Id, UpdateForecastDto Dto) : IRequest<Response<string>>;

public class UpdateForecastHandler : IRequestHandler<UpdateForecastCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly ICommonQueries _commonQueries;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateForecastHandler(ICommonRepository commonRepository, 
        ICommonQueries commonQueries, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _commonQueries = commonQueries;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(UpdateForecastCommand command, CancellationToken token = default)
    {
        // Check current user
        var currentUser = await _commonQueries.GetCurrentUser(token);
        if(!currentUser.UserFound)
            return new Response<string>()
                .WithMessage("Error updating forecast.")
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
        var forecastFilters = new Expression<Func<Forecast, bool>>[] { x => x.Id == command.Id};
        var forecast = await _commonRepository.GetResultByIdAsync(forecastFilters, token: token);

        // Check for existence
        if (forecast is null)
        {
            return new Response<string>()
                .WithMessage("Error updating forecast.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Forecast not found");
        }

        // Check for concurrency issues
        if (forecast.IsLockedByOtherUser(currentUser.Id))
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity is currently locked.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData(@$"The forecast has been modified by another {forecast.LockedByUser!.UserName}. 
                    Please try again.");
        }
            
        // Check for concurrency issues
        if (forecast.Version != command.Dto.Version)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Concurrency conflict.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData("The role has been modified by another user. Please try again.");
        }

        // Searching Item
        var warningFilters = new Expression<Func<Warning, bool>>[] { x => x.Id == command.Dto.WarningId};
        var warning = await _commonRepository.GetResultByIdAsync(warningFilters, token: token);
        
        // Check for existence
        if (warning is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating forecast.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Warning not found");
        }

        // Searching Item
        var locationFilters = new Expression<Func<Location, bool>>[] { l => l.Id == command.Dto.LocationId};
        var location = await _commonRepository.GetResultByIdAsync(locationFilters, token: token);
        
        // Check for existence
        if (location is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating forecast.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Location not found");
        }

        // Searching Item
        var moonPhaseFilters = new Expression<Func<MoonPhase, bool>>[] { l => l.Id == command.Dto.MoonPhaseId};
        var moonPhase = await _commonRepository.GetResultByIdAsync(moonPhaseFilters, token: token);
        
        // Check for existence
        if (moonPhase is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating forecast.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Moon phase not found");
        }

        // Mapping, Validating, Saving Item
        command.Dto.UpdateForecastModelMapping(forecast, warning, 
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
        var result = await _commonRepository.UpdateAsync(forecast, token);

        // Saving failed.
        if(!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating forecast.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to update forecast.");
        }

        // Unlock result
        var unlockResult = await _commonRepository.UnlockAsync<Forecast>(forecast.Id, token);
        if(!unlockResult)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error updating forecast.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(unlockResult)
                .WithData("Failed to update forecast.");
        }

        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);
            
        // Initializing object
        return new Response<string>()
            .WithMessage("Success updating forecast.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithData($"Forecast {forecast.Date.GetLocalDateString()} updated successfully!");
    }
}