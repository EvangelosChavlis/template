// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Geography.Administrative.Regions.Models;

namespace server.src.Application.Geography.Administrative.Regions.Filters;

public static class RegionFilters
{
    public static string RegionNameSorting = 
        typeof(Region).GetProperty(nameof(Region.Name))!.Name;

    public static Expression<Func<Region, bool>> RegionSearchFilter(this string filter)
    {
        return o => o.Name.Contains(filter ?? "") ||
            o.Description.Contains(filter ?? "");
    }
}