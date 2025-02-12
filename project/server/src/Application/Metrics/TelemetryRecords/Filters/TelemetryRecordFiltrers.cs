// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Metrics.TelemetryRecords.Models;

namespace server.src.Application.TelemetryRecords.Filters;

public static class TelemetryRecordFiltrers
{
    public static string TelemetryRecordNameSorting = typeof(TelemetryRecord).GetProperty(nameof(TelemetryRecord.Path))!.Name;

    public static Expression<Func<TelemetryRecord, bool>> TelemetryRecordSearchFilter(this string filter)
    {
        return t => t.Method.Contains(filter ?? "") ||
                t.Path.Contains(filter ?? "") ||
                t.StatusCode.ToString().Contains(filter ?? "") ||
                t.ResponseTime.ToString().Contains(filter ?? "") ||
                t.RequestTimestamp.ToString().Contains(filter ?? "");
    }

    public static Expression<Func<TelemetryRecord, bool>>[] TelemetryRecordMatchFilters(this Expression<Func<TelemetryRecord, bool>>? filter, Guid userId)
    {
        var filters = new Expression<Func<TelemetryRecord, bool>>[]
        {
            filter!,
            x => x.UserId == userId
        };

        return filters;
    }
}
