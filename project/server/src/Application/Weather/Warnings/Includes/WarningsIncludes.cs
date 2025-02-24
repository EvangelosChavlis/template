// source
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Warnings.Models;

namespace server.src.Application.Weather.Warnings.Includes;

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