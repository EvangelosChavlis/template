// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Municipalities.Models;

namespace server.src.Application.Geography.Administrative.Municipalities.Includes;

public class MunicipalitiesIncludes
{
    public static IncludeThenInclude<Municipality>[] GetMunicipalitiesIncludes()
    {
        return
        [
            new (m => m.Districts)
        ];
    }
}