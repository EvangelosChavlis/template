// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Districts.Models;

namespace server.src.Application.Geography.Administrative.Districts.Includes;

public class DistrictsIncludes
{
    public static IncludeThenInclude<District>[] GetDistrictsIncludes()
    {
        return
        [
            new (d => d.Neighborhoods)
        ];
    }
}