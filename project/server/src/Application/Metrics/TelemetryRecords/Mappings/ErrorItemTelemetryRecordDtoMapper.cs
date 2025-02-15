// source
using server.src.Domain.Metrics.TelemetryRecords.Dtos;

namespace server.src.Application.Metrics.TelemetryRecords.Mappings;

/// <summary>
/// Provides a method to generate a default error representation of an <see cref="ItemTelemetryRecordDto"/> 
/// with placeholder values for error handling scenarios.
/// </summary>
public static class ErrorItemTelemetryRecordDtoMapper
{
    /// <summary>
    /// Creates an <see cref="ItemTelemetryRecordDto"/> with default empty or invalid values, 
    /// typically used in error scenarios where valid telemetry data is unavailable.
    /// </summary>
    /// <returns>An <see cref="ItemTelemetryRecordDto"/> containing placeholder values.</returns>
    public static ItemTelemetryRecordDto ErrorItemTelemetryRecordDtoMapping()
        => new(
            Id: Guid.Empty,
            Method: string.Empty,
            Path: string.Empty,
            StatusCode: "0",
            ResponseTime: 0,
            MemoryUsed: 0,
            CPUusage: 0,
            RequestBodySize: 0,
            RequestTimestamp: string.Empty,
            ResponseBodySize: 0,
            ResponseTimestamp: string.Empty,
            ClientIp: string.Empty,
            UserAgent: string.Empty,
            ThreadId: string.Empty,
            UserId: Guid.Empty,
            UserName: string.Empty
        );
}