// source

using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Observations.Models;

namespace server.src.Application.Weather.Observations;

public class ObservationsIncludes
{
    public static IncludeThenInclude<Observation>[] GetObservationsIncludes()
    {
        return
        [
            new (f => f.MoonPhase),
            new (f => f.Location)
        ];
    }
}