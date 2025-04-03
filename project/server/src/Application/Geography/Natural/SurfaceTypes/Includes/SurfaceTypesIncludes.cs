// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.SurfaceTypes.Models;

namespace server.src.Application.Geography.Natural.Includes.SurfaceTypes;

public class SurfaceTypesIncludes
{
    public static IncludeThenInclude<SurfaceType>[] GetSurfaceTypesIncludes()
    {
        return
        [
            new (t => t.Locations)
        ];
    }
}