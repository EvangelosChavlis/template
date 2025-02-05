namespace server.src.Domain.Metrics.LogErrors.Dtos;

/// <summary>
/// Represents a simplified error item that includes basic error details
/// such as an ID, error message, status code, and timestamp.
/// </summary>
public record ListItemLogErrorDto(
    Guid Id, 
    string Error, 
    int StatusCode,
    string Timestamp
);
