// source
using server.src.Domain.Dto.Metrics;
using server.src.Domain.Extensions;
using server.src.Domain.Models.Errors;

namespace server.src.Application.Mappings.Metrics;

/// <summary>
/// Contains static mapping methods to transform between LogError models and their corresponding DTOs.
/// </summary>
public static class ErrorsMappings
{
    /// <summary>
    /// Maps a LogError model to a ListItemErrorDto.
    /// </summary>
    /// <param name="model">The LogError model that will be mapped to a ListItemErrorDto.</param>
    /// <returns>A ListItemErrorDto representing the LogError model.</returns>
    public static ListItemErrorDto ListItemErrorDtoMapping(
        this LogError model) => new(
            Id: model.Id,
            Error: model.Error,
            StatusCode: model.StatusCode,
            Timestamp: model.Timestamp.GetFullLocalDateTimeString()
        );

    /// <summary>
    /// Maps a LogError model to an ItemErrorDto.
    /// </summary>
    /// <param name="model">The LogError model that will be mapped to an ItemErrorDto.</param>
    /// <returns>An ItemErrorDto representing the LogError model.</returns>
    public static ItemErrorDto ItemErrorDtoMapping(
        this LogError model) => new(
            Id: model.Id,
            Error: model.Error,
            StatusCode: model.StatusCode,
            Instance: model.Instance,
            ExceptionType: model.ExceptionType,
            StackTrace: model.StackTrace,
            Timestamp: model.Timestamp.GetFullLocalDateTimeString()
        );

    /// <summary>
    /// Maps a Warning model to an ItemErrorDto with default values in case of an error.
    /// </summary>
    /// <param name="model">The Error model that will be mapped to an ItemErrorDto.</param>
    /// <returns>An ItemWarningDto with default values representing an error state.</returns>
    public static ItemErrorDto ItemErrorDtoMapping() => 
        new(
            Id: Guid.Empty,
            Error: string.Empty,
            StatusCode: int.MinValue,
            Instance: string.Empty,
            ExceptionType: string.Empty,
            StackTrace: string.Empty,
            Timestamp: string.Empty
        );
}
