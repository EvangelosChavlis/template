// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Geography.Administrative.Districts.Models;

namespace server.src.Application.Geography.Administrative.Districts.Filters;

public static class DistrictFilters
{
    public static string DistrictNameSorting = 
        typeof(District).GetProperty(nameof(District.Name))!.Name;

    public static Expression<Func<District, bool>> DistrictSearchFilter(this string filter)
    {
        return o => o.Name.Contains(filter ?? "") ||
            o.Description.Contains(filter ?? "");
    }
}