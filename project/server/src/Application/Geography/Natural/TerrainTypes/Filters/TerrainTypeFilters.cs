// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Geography.Natural.TerrainTypes.Models;

namespace server.src.Application.Geography.Natural.TerrainTypes.Filters;

public static class TerrainTypeFilters
{
    public static string TerrainTypeNameSorting = typeof(TerrainType).GetProperty(nameof(TerrainType.Name))!.Name;

    public static Expression<Func<TerrainType, bool>> TerrainTypeSearchFilter(this string filter)
    {
        return o => o.Name.Contains(filter ?? "") ||
            o.Description.Contains(filter ?? "");
    }
}