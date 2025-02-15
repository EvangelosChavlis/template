// source
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.MoonPhases.Models;

namespace server.src.Application.Weather.MoonPhases;

public class MoonPhasesIncludes
{
    public static IncludeThenInclude<MoonPhase>[] GetMoonPhasesIncludes()
    {
        return
        [
            new (v => v.Forecasts)
        ];
    }
}