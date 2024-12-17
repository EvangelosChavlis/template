// source
using server.src.Domain.Models.Common;
using server.src.Domain.Models.Weather;

namespace server.src.Application.Includes.Weather;

public class WarningsIncludes
{
    public static IncludeThenInclude<Warning>[] GetWarningsIncludes()
    {
        return
        [
            new (v => v.Forecasts)
        ];
    }
}