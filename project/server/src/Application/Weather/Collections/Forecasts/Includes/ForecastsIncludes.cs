// source

using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Collections.Forecasts.Models;

namespace server.src.Application.Weather.Collections.Forecasts;

public class ForecastsIncludes
{
    public static IncludeThenInclude<Forecast>[] GetForecastsIncludes()
    {
        return
        [
            new (f => f.Warning),
            new (f => f.MoonPhase),
            new (f => f.Station)
        ];
    }
}