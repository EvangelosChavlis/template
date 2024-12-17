// source
using server.src.Domain.Models.Common;
using server.src.Domain.Models.Weather;

namespace server.src.Application.Includes.Weather;

public class ForecastsIncludes
{
    public static IncludeThenInclude<Forecast>[] GetForecastsIncludes()
    {
        return
        [
            new (v => v.Warning)
        ];
    }
}