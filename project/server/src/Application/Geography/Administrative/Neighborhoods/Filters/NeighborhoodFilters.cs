// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;

namespace server.src.Application.Geography.Administrative.Neighborhoods.Filters;

public static class NeighborhoodFilters
{
    public static string NeighborhoodNameSorting = 
        typeof(Neighborhood).GetProperty(nameof(Neighborhood.Name))!.Name;

    public static Expression<Func<Neighborhood, bool>> NeighborhoodSearchFilter(this string filter)
    {
        return o => o.Name.Contains(filter ?? "") ||
            o.Description.Contains(filter ?? "");
    }
}