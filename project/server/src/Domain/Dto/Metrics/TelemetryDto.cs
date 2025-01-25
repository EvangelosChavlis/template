namespace server.src.Domain.Dto.Metrics;

/// <summary>
/// Represents a simplified telemetry item containing basic information
/// about the request method, path, status code, response time, and timestamp.
/// </summary>
public record ListItemTelemetryDto(
    Guid Id, 
    string Method, 
    string Path,
    string StatusCode,
    long ResponseTime,
    string RequestTimestamp
);

/// <summary>
/// Represents a detailed telemetry item containing comprehensive information
/// about the request, response, and system performance metrics.
/// </summary>
public record ItemTelemetryDto(
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
