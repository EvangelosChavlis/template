// source

using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Forecasts.Models;

namespace server.src.Application.Weather.Forecasts;

public class ForecastsIncludes
{
    public static IncludeThenInclude<Forecast>[] GetForecastsIncludes()
    {
        return
        [
            new (f => f.Warning),
            new (f => f.MoonPhase),
            new (f => f.Location)
        ];
    }
}