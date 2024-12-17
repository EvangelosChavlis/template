namespace server.src.Domain.Models.Errors;

/// <summary>
/// Represents detailed information about an error that occurred in the system.
/// This class is typically used for logging or returning error details in API responses.
/// </summary>
public class ErrorDetails
{
    /// <summary>
    /// Gets or sets the error message describing the nature of the error.
    /// </summary>
    public string Error { get; set; }

    /// <summary>
    /// Gets or sets the HTTP status code associated with the error.
    /// This status code can indicate the type of error, such as 404 for "Not Found" or 500 for "Internal Server Error".
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Gets or sets the instance identifier, which is typically used to track the specific occurrence of the error.
    /// </summary>
    public string Instance { get; set; }

    /// <summary>
    /// Gets or sets the type of the exception that caused the error (e.g., `NullReferenceException`, `ArgumentException`).
    /// </summary>
    public string ExceptionType { get; set; }

    /// <summary>
    /// Gets or sets the stack trace of the exception, which provides detailed information about the sequence of method calls leading to the error.
    /// This is helpful for debugging.
    /// </summary>
    public string StackTrace { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of when the error occurred. This can be useful for tracking when issues happen and for logging purposes.
    /// </summary>
    public DateTime Timestamp { get; set; }
}