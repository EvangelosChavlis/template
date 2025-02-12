// source
using server.src.Domain.Common.Models;
using server.src.Domain.Metrics.TelemetryRecords.Models;

namespace server.src.Application.TelemetryRecords.Includes;

public class TelemetryRecordIncludes
{
    public static IncludeThenInclude<TelemetryRecord>[] GetTelemetryRecordIncludes()
    {
        return [];
        {}
    }
}