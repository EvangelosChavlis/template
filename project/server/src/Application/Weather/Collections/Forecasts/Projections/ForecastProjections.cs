// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Weather.Collections.Forecasts.Models;

namespace server.src.Application.Weather.Collections.Forecasts.Projections;

public static class ForecastProjections
{
    public static Expression<Func<Forecast, Forecast>> GetForecastsProjection()
    {
        return f => new Forecast
        {
            Id = f.Id,
            Date = f.Date,
            TemperatureC = f.TemperatureC,
            Humidity = f.Humidity,
            WarningId = f.WarningId,
            Warning = f.Warning,
            MoonPhaseId = f.MoonPhaseId,
            MoonPhase = f.MoonPhase,
            StationId = f.StationId,
            Station = f.Station,
        };
    }
}