namespace server.src.Domain.Models.Errors;

/// <summary>
/// Represents an error log entry in the system. This class is typically used to store error details for logging purposes.
/// </summary>
public class LogError
{
    /// <summary>
    /// Gets or sets the unique identifier for the error log entry.
    /// This can be used to uniquely identify an error in a logging system or database.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the error message describing the nature of the error.
    /// This message provides a brief explanation of the error that occurred.
    /// </summary>
    public string Error { get; set; }

    /// <summary>
    /// Gets or sets the HTTP status code associated with the error.
    /// The status code can represent the type of error, such as 404 (Not Found) or 500 (Internal Server Error).
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Gets or sets the instance identifier, which is used to track the specific occurrence of the error.
    /// This could represent a request ID or a unique identifier for the error instance.
    /// </summary>
    public string Instance { get; set; }

    /// <summary>
    /// Gets or sets the type of exception that caused the error.
    /// For example, it could be `NullReferenceException`, `ArgumentException`, etc.
    /// </summary>
    public string ExceptionType { get; set; }

    /// <summary>
    /// Gets or sets the stack trace of the exception.
    /// The stack trace provides detailed information about the method calls leading to the error.
    /// </summary>
    public string StackTrace { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the error occurred.
    /// This helps to track when the error was logged and can be useful for debugging or historical records.
    /// </summary>
    public DateTime Timestamp { get; set; }
}