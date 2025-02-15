// source
using server.src.Domain.Common.Extensions;
using server.src.Domain.Metrics.TelemetryRecords.Dtos;
using server.src.Domain.Metrics.TelemetryRecords.Models;

namespace server.src.Application.Metrics.TelemetryRecords.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="TelemetryRecord"/> 
/// into an <see cref="ItemTelemetryRecordDto"/> for detailed individual item representation.
/// </summary>
public static class ItemTelemetryRecordDtoMapper
{
    /// <summary>
    /// Converts a <see cref="TelemetryRecord"/> into an <see cref="ItemTelemetryRecordDto"/>, 
    /// capturing full telemetry details for an individual record view.
    /// </summary>
    /// <param name="model">The telemetry record to be mapped.</param>
    /// <returns>A DTO containing comprehensive telemetry details.</returns>
    public static ItemTelemetryRecordDto ItemTelemetryRecordDtoMapping(
        this TelemetryRecord model) => new(
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
}