// source
using server.src.Domain.Models.Common;
using server.src.Domain.Models.Metrics;

namespace server.src.Application.Includes.Weather;

public class TelemetryIncludes
{
    public static IncludeThenInclude<Telemetry>[] GetTelemetryIncludes()
    {
        return [];
        {}
    }
}