// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Stations.Models;

namespace server.src.Application.Geography.Administrative.Includes.Stations;

public class StationsIncludes
{
    public static IncludeThenInclude<Station>[] GetStationsIncludes()
    {
        return
        [
            new (s => s.Observations),
            new (s => s.Forecasts)
        ];
    }
}