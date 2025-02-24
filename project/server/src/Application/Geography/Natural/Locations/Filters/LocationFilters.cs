// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Geography.Natural.Locations.Models;

namespace server.src.Application.Geography.Natural.Locations.Filters;

public static class LocationFilters
{
    public static string LocationSorting = typeof(Location).GetProperty(nameof(Location.Id))!.Name;

    public static Expression<Func<Location, bool>> LocationSearchFilter(this string filter)
    {
        return l => l.Latitude.ToString().Contains(filter ?? "") ||
            l.Longitude.ToString().Contains(filter ?? "") ||
            l.Altitude.ToString().Contains(filter ?? "");
    }
}