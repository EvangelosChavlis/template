// source
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Collections.Warnings.Models;

namespace server.src.Application.Weather.Collections.Warnings.Includes;

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