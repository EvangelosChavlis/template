// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Geography.Natural.ClimateZones.Models;

namespace server.src.Application.Geography.Natural.ClimateZones.Filters;

public static class ClimateZoneFilters
{
    public static string ClimateZoneNameSorting = typeof(ClimateZone).GetProperty(nameof(ClimateZone.Name))!.Name;

    public static Expression<Func<ClimateZone, bool>> ClimateZoneSearchFilter(this string filter)
    {
        return o => o.Name.Contains(filter ?? "") ||
            o.Description.Contains(filter ?? "");
    }
}