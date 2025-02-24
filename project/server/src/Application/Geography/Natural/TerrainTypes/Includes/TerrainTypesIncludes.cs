// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.TerrainTypes.Models;

namespace server.src.Application.Geography.Natural.Includes.TerrainTypes;

public class TerrainTypesIncludes
{
    public static IncludeThenInclude<TerrainType>[] GetTerrainTypesIncludes()
    {
        return
        [
            new (t => t.Locations)
        ];
    }
}