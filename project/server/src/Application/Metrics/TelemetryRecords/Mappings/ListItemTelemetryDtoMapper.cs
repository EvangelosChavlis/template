// source
using server.src.Domain.Common.Extensions;
using server.src.Domain.Metrics.TelemetryRecords.Dtos;
using server.src.Domain.Metrics.TelemetryRecords.Models;

namespace server.src.Application.Metrics.TelemetryRecords.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="TelemetryRecord"/> 
/// into a <see cref="ListItemTelemetryRecordDto"/> for list view representation.
/// </summary>
public static class ListItemTelemetryDtoMapper
{
    /// <summary>
    /// Converts a <see cref="TelemetryRecord"/> into a <see cref="ListItemTelemetryRecordDto"/>, 
    /// extracting key details relevant for a summarized list view.
    /// </summary>
    /// <param name="model">The telemetry record to be mapped.</param>
    /// <returns>A DTO containing essential telemetry details.</returns>
    public static ListItemTelemetryRecordDto ListItemTelemetryRecordDtoMapping(
        this TelemetryRecord model) => new(
            Id: model.Id,
            Method: model.Method,
            Path: model.Path,
            StatusCode: model.StatusCode.ToString(),
            ResponseTime: model.ResponseTime,
            RequestTimestamp: model.RequestTimestamp.GetFullLocalDateTimeString()
        );
}
