// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Geography.Administrative.Continents.Models;

namespace server.src.Application.Geography.Administrative.Continents.Filters;

public static class ContinentFilters
{
    public static string ContinentNameSorting = 
        typeof(Continent).GetProperty(nameof(Continent.Name))!.Name;

    public static Expression<Func<Continent, bool>> ContinentSearchFilter(this string filter)
    {
        return o => o.Name.Contains(filter ?? "") ||
            o.Description.Contains(filter ?? "");
    }
}