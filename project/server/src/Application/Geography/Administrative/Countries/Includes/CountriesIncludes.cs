// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Countries.Models;

namespace server.src.Application.Geography.Administrative.Countries.Includes;

public class CountriesIncludes
{
    public static IncludeThenInclude<Country>[] GetCountrysIncludes()
    {
        return
        [
            new (c => c.States)
        ];
    }
}