// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.ClimateZones.Models;

namespace server.src.Application.Geography.Natural.Includes.ClimateZones;

public class ClimateZonesIncludes
{
    public static IncludeThenInclude<ClimateZone>[] GetClimateZonesIncludes()
    {
        return
        [
            new (t => t.Locations)
        ];
    }
}