namespace server.src.Domain.Dto.Metrics;

/// <summary>
/// Represents a simplified error item that includes basic error details
/// such as an ID, error message, status code, and timestamp.
/// </summary>
public record ListItemErrorDto(
    Guid Id, 
    string Error, 
    int StatusCode,
    string Timestamp
);

/// <summary>
/// Represents a detailed error item that includes additional information
/// such as instance, exception type, stack trace, and other error details.
/// </summary>
public record ItemErrorDto(
    Guid Id, 
    string Error, 
    int StatusCode,
    string Instance,
    string ExceptionType,
    string StackTrace,
    string Timestamp
);
