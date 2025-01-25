// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Interfaces;
using server.src.Application.Mappings.Weather;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Domain.Extensions;
using server.src.Domain.Models.Weather;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Weather.Forecasts.Commands;

public record UpdateForecastCommand(Guid Id, ForecastDto Dto) : IRequest<Response<string>>;

public class UpdateForecastHandler : IRequestHandler<UpdateForecastCommand, Response<string>>
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;

    public UpdateForecastHandler(DataContext context, ICommonRepository commonRepository)
    {
        _context = context;
        _commonRepository = commonRepository;
    }

    public async Task<Response<string>> Handle(UpdateForecastCommand command, CancellationToken token = default)
    {
        // Searching Item (Warning)
        var warningIncludes = new Expression<Func<Warning, object>>[] {  };
        var warningFilters = new Expression<Func<Warning, bool>>[] { x => x.Id == command.Dto.WarningId};
        var warning = await _commonRepository.GetResultByIdAsync(_context.Warnings, warningFilters, warningIncludes, token);
        
        if (warning is null)
            return new Response<string>()
                .WithMessage("Error updating forecast.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Warning not found");

        // Searching Item (forecast)
        var forecastIncludes = new Expression<Func<Forecast, object>>[] {  };
        var forecastFilters = new Expression<Func<Forecast, bool>>[] { x => x.Id == command.Id};
        var forecast = await _commonRepository.GetResultByIdAsync(_context.Forecasts, forecastFilters, forecastIncludes, token);

        if (forecast is null)
            return new Response<string>()
                .WithMessage("Error updating forecast.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Forecast not found");

        // Mapping and Saving
        command.Dto.UpdateForecastMapping(forecast, warning);
        var result = await _context.SaveChangesAsync(token) > 0;

       if(!result)
            return new Response<string>()
                .WithMessage("Error updating forecast.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to update forecast.");

        // Initializing object
        return new Response<string>()
            .WithMessage("Success updating forecast.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithData($"Forecast {forecast.Date.GetLocalDateString()} updated successfully!");
    }
}