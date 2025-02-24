// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Geography.Natural.Timezones.Models;

namespace server.src.Application.Geography.Natural.Timezones.Filters;

public static class TimezoneFilters
{
    public static string TimezoneNameSorting = typeof(Timezone).GetProperty(nameof(Timezone.Name))!.Name;

    public static Expression<Func<Timezone, bool>> TimezoneSearchFilter(this string filter)
    {
        return o => o.Name.Contains(filter ?? "") ||
            o.Description.Contains(filter ?? "");
    }
}