// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;

namespace server.src.Application.Geography.Administrative.Neighborhoods.Includes;

public class NeighborhoodsIncludes
{
    public static IncludeThenInclude<Neighborhood>[] GetNeighborhoodsIncludes()
    {
        return
        [
            new (n => n.Locations)
        ];
    }
}