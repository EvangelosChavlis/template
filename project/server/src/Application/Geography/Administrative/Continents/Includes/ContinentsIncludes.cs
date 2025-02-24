// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Continents.Models;

namespace server.src.Application.Geography.Administrative.Includes.Continents;

public class ContinentsIncludes
{
    public static IncludeThenInclude<Continent>[] GetContinentsIncludes()
    {
        return
        [
            new (c => c.Countries)
        ];
    }
}