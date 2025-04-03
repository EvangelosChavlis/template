// source

using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Collections.Observations.Models;

namespace server.src.Application.Weather.Collections.Observations;

public class ObservationsIncludes
{
    public static IncludeThenInclude<Observation>[] GetObservationsIncludes()
    {
        return
        [
            new (f => f.MoonPhase),
            new (f => f.Station)
        ];
    }
}