// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Countries.Models;

namespace server.src.Application.Geography.Administrative.Includes.Countries;

public class CountrysIncludes
{
    public static IncludeThenInclude<Country>[] GetCountrysIncludes()
    {
        return
        [
            new (c => c.States)
        ];
    }
}