// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Metrics.TelemetryRecords.Models;

namespace server.src.Application.Metrics.TelemetryRecords.Projections;

public static class TelemetryRecordProjections
{
    public static Expression<Func<TelemetryRecord, TelemetryRecord>> GetTelemetryRecordsProjection()
    {
        return t => new TelemetryRecord
        {
            Id = t.Id,
            Method = t.Method,
            Path = t.Path,
            StatusCode = t.StatusCode,
            ResponseTime = t.ResponseTime,
            RequestTimestamp = t.RequestTimestamp
        };
    }
}