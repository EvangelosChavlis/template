// source
using server.src.Domain.Dto.Metrics;
using server.src.Domain.Extensions;
using server.src.Domain.Models.Errors;

namespace server.src.Application.Mappings.Metrics;

public static class ErrorsMappings
{
    public static ListItemErrorDto ListItemErrorDtoMapping(
        this LogError model) => new(
            Id: model.Id,
            Error: model.Error,
            StatusCode: model.StatusCode,
            Timestamp: model.Timestamp.GetFullLocalDateTimeString()
        );


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
}