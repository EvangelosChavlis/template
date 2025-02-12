// source
using server.src.Domain.Common.Extensions;
using server.src.Domain.Metrics.LogErrors.Dtos;
using server.src.Domain.Metrics.LogErrors.Models;

namespace server.src.Application.Metrics.LogErrors.Mappings;

public static class ListItemLogErrorDtoMapper
{
    /// <summary>
    /// Maps a LogError model to a ListItemLogErrorDto.
    /// This method is typically used for returning simplified error data, 
    /// such as in list or summary views of error logs.
    /// </summary>
    /// <param name="model">The LogError model that will be mapped to a ListItemLogErrorDto.</param>
    /// <returns>A ListItemLogErrorDto representing the LogError model, containing properties 
    /// like Id, Error message, StatusCode, and Timestamp in a readable format.</returns>
    public static ListItemLogErrorDto ListItemLogErrorDtoMapping(
        this LogError model) => new(
            Id: model.Id,
            Error: model.Error,
            StatusCode: model.StatusCode,
            Timestamp: model.Timestamp.GetFullLocalDateTimeString()
        );
}
