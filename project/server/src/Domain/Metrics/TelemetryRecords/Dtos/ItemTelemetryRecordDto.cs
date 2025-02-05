namespace server.src.Domain.Metrics.TelemetryRecords.Dtos;

/// <summary>
/// Represents a detailed telemetry item containing comprehensive information
/// about the request, response, and system performance metrics.
/// </summary>
public record ItemTelemetryRecordDto(
    Guid Id,
    string Method,
    string Path,
    string StatusCode,
    long ResponseTime,
    long MemoryUsed,
    double CPUusage,
    long RequestBodySize,
    string RequestTimestamp,
    long ResponseBodySize,
    string ResponseTimestamp,
    string ClientIp,
    string UserAgent,
    string ThreadId,
    Guid UserId,
    string UserName
);