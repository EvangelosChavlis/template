// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Geography.Natural.SurfaceTypes.Models;

namespace server.src.Application.Geography.Natural.SurfaceTypes.Filters;

public static class SurfaceTypeFilters
{
    public static string SurfaceTypeNameSorting = typeof(SurfaceType).GetProperty(nameof(SurfaceType.Name))!.Name;

    public static Expression<Func<SurfaceType, bool>> SurfaceTypeSearchFilter(this string filter)
    {
        return s => s.Name.Contains(filter ?? "") ||
            s.Description.Contains(filter ?? "");
    }
}