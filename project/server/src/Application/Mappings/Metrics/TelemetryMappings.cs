// source
using server.src.Domain.Dto.Metrics;
using server.src.Domain.Extensions;
using server.src.Domain.Models.Metrics;

namespace server.src.Application.Mappings.Metrics;

public static class TelemetryMappings
{
    public static ListItemTelemetryDto ListItemTelemetryDtoMapping(
        this Telemetry model) => new(
            Id: model.Id,
            Method: model.Method,
            Path: model.Path,
            StatusCode: model.StatusCode.ToString(),
            ResponseTime: model.ResponseTime,
            RequestTimestamp: model.RequestTimestamp.GetFullLocalDateTimeString()
        );


   public static ItemTelemetryDto ItemTelemetryDtoMapping(
        this Telemetry model) => new(
            Id: model.Id,
            Method: model.Method,
            Path: model.Path,
            StatusCode: model.StatusCode.ToString(),
            ResponseTime: model.ResponseTime,
            MemoryUsed: model.MemoryUsed,
            CPUusage: model.CPUusage,
            RequestBodySize: model.RequestBodySize,
            RequestTimestamp: model.RequestTimestamp.GetFullLocalDateTimeString(),
            ResponseBodySize: model.ResponseBodySize,
            ResponseTimestamp: model.RequestTimestamp.GetFullLocalDateTimeString(),
            ClientIp: model.ClientIp,
            UserAgent: model.UserAgent,
            ThreadId: model.ThreadId
        );
}