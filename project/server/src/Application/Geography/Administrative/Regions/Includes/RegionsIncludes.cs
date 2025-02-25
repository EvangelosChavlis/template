// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Regions.Models;

namespace server.src.Application.Geography.Administrative.Regions.Includes;

public class RegionsIncludes
{
    public static IncludeThenInclude<Region>[] GetRegionsIncludes()
    {
        return
        [
            new (r => r.Municipalities)
        ];
    }
}