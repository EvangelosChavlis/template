// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Models.Metrics;

namespace server.src.Application.Filters.Metrics;

public static class TelemetryFiltrers
{
    public static string TelemetryNameSorting = typeof(Telemetry).GetProperty(nameof(Telemetry.Path))!.Name;

    public static Expression<Func<Telemetry, bool>> TelemetrySearchFilter(this string filter)
    {
        return t => t.Method.Contains(filter ?? "") ||
                t.Path.Contains(filter ?? "") ||
                t.StatusCode.ToString().Contains(filter ?? "") ||
                t.ResponseTime.ToString().Contains(filter ?? "") ||
                t.RequestTimestamp.ToString().Contains(filter ?? "");
    }

    public static Expression<Func<Telemetry, bool>>[] TelemetryMatchFilters(this Expression<Func<Telemetry, bool>>? filter, Guid userId)
    {
        var filters = new Expression<Func<Telemetry, bool>>[]
        {
            filter!,
            x => x.UserId == userId
        };

        return filters;
    }
}
