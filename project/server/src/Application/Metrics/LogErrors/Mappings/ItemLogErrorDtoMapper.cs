// source
using server.src.Domain.Common.Extensions;
using server.src.Domain.Metrics.LogErrors.Dtos;
using server.src.Domain.Metrics.LogErrors.Models;

namespace server.src.Application.Metrics.LogErrors.Mappings;

/// <summary>
/// Provides mapping functionality to convert a LogError model to an ItemLogErrorDto.
/// This mapping is useful when more detailed error information is needed, typically 
/// for showing individual error records with full details in a detailed view.
/// </summary>
public static class ItemLogErrorDtoMapper
{
    /// <summary>
    /// Maps a LogError model to an ItemLogErrorDto.
    /// This method is used for converting a LogError model into a data transfer object 
    /// that contains detailed information such as error message, status code, 
    /// exception type, stack trace, and timestamp.
    /// </summary>
    /// <param name="model">The LogError model that will be mapped to an ItemLogErrorDto.</param>
    /// <returns>An ItemLogErrorDto representing the LogError model with full details.</returns>
    public static ItemLogErrorDto ItemLogErrorDtoMapping(
        this LogError model) => new(
            Id: model.Id,
            Error: model.Error,
            StatusCode: model.StatusCode,
            Instance: model.Instance,
            ExceptionType: model.ExceptionType,
            StackTrace: model.StackTrace,
            Timestamp: model.Timestamp.GetFullLocalDateTimeString()
        );
}
