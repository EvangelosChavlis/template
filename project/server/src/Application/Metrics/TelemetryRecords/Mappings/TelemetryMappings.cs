// source
using server.src.Domain.Dto.Metrics;
using server.src.Domain.Extensions;
using server.src.Domain.Models.Metrics;

namespace server.src.Application.Metrics.Telemetry.Mappings;

/// <summary>
/// Contains static mapping methods to transform Telemetry models into their corresponding DTOs.
/// </summary>
public static class TelemetryMappings
{
    /// <summary>
    /// Maps a Telemetry model to a ListItemTelemetryDto.
    /// </summary>
    /// <param name="model">The Telemetry model that will be mapped to a ListItemTelemetryDto.</param>
    /// <returns>A ListItemTelemetryDto representing the Telemetry model with key details for a list view.</returns>
    public static ListItemTelemetryDto ListItemTelemetryDtoMapping(
        this Telemetry model) => new(
            Id: model.Id,
            Method: model.Method,
            Path: model.Path,
            StatusCode: model.StatusCode.ToString(),
            ResponseTime: model.ResponseTime,
            RequestTimestamp: model.RequestTimestamp.GetFullLocalDateTimeString()
        );

    /// <summary>
    /// Maps a Telemetry model to an ItemTelemetryDto.
    /// </summary>
    /// <param name="model">The Telemetry model that will be mapped to an ItemTelemetryDto.</param>
    /// <returns>An ItemTelemetryDto representing the Telemetry model with full details for an individual item view.</returns>
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
            ThreadId: model.ThreadId,
            UserId: model.User.Id,
            UserName: model.User.UserName!
        );

    /// <summary>
    /// Generates an error ItemTelemetryDto with default empty or invalid values.
    /// </summary>
    /// <returns>An ItemTelemetryDto containing default values, typically used for error scenarios.</returns>
    public static ItemTelemetryDto ErrorItemTelemetryDtoMapping()
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