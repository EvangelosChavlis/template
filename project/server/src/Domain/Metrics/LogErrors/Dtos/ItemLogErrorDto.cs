namespace server.src.Domain.Metrics.LogErrors.Dtos;

/// <summary>
/// Represents a detailed error item that includes additional information
/// such as instance, exception type, stack trace, and other error details.
/// </summary>
public record ItemLogErrorDto(
    Guid Id, 
    string Error, 
    int StatusCode,
    string Instance,
    string ExceptionType,
    string StackTrace,
    string Timestamp
);