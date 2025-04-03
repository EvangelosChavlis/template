// source
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Collections.MoonPhases.Models;

namespace server.src.Application.Weather.Collections.MoonPhases;

public class MoonPhasesIncludes
{
    public static IncludeThenInclude<MoonPhase>[] GetMoonPhasesIncludes()
    {
        return
        [
            new (m => m.Forecasts),
            new (m => m.Observations)
        ];
    }
}