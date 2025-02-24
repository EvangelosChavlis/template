// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Geography.Administrative.Countries.Models;

namespace server.src.Application.Geography.Administrative.Countries.Filters;

public static class CountryFilters
{
    public static string CountryNameSorting = 
        typeof(Country).GetProperty(nameof(Country.Name))!.Name;

    public static Expression<Func<Country, bool>> CountrySearchFilter(this string filter)
    {
        return o => o.Name.Contains(filter ?? "") ||
            o.Description.Contains(filter ?? "");
    }
}