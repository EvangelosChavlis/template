namespace server.src.Domain.Common.Models;

/// <summary>
/// Represents a custom exception that can be thrown to indicate application-specific errors.
/// This exception allows associating an HTTP status code with the error, useful in web APIs.
/// </summary>
public class CustomException : Exception
{
    /// <summary>
    /// Gets the HTTP status code associated with the exception.
    /// This can be used to return an appropriate status code in web API responses.
    /// </summary>
    public int StatusCode { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomException"/> class with a specific error message and optional status code.
    /// The default status code is 400 (Bad Request) if no status code is provided.
    /// </summary>
    /// <param name="message">The error message that describes the exception.</param>
    /// <param name="statusCode">The HTTP status code associated with the exception (default is 400).</param>
    public CustomException(string message, int statusCode = 400) : base(message)
    {
        StatusCode = statusCode;
    }
}