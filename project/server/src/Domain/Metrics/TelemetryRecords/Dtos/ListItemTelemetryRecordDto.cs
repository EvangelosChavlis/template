namespace server.src.Domain.Metrics.TelemetryRecords.Dtos;

/// <summary>
/// Represents a simplified telemetry item containing basic information
/// about the request method, path, status code, response time, and timestamp.
/// </summary>
public record ListItemTelemetryRecordDto(
    Guid Id, 
    string Method, 
    string Path,
    string StatusCode,
    long ResponseTime,
    string RequestTimestamp
);