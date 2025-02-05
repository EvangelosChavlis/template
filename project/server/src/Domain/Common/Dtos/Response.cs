namespace server.src.Domain.Common.Dtos;

/// <summary>
/// Represents a standard response for commands that perform an action and may return data.
/// </summary>
/// <typeparam name="T">The type of data associated with the response.</typeparam>
public class Response<T>
{
    /// <summary>
    /// Gets or sets the data associated with the response.
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the command was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the HTTP status code for the response.
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Gets or sets a message that provides additional information about the response.
    /// This can be a success or error message.
    /// </summary>
    public string? Message { get; set; }


    /// <summary>
    /// Assigns the provided data to the response and returns the updated instance.
    /// </summary>
    /// <param name="data">The data to associate with the response.</param>
    /// <returns>The updated <see cref="Response{T}"/> instance.</returns>
    public Response<T> WithData(T data)
    {
        Data = data;
        return this;
    }

    /// <summary>
    /// Assigns the success status to the response and returns the updated instance.
    /// </summary>
    /// <param name="success">A value indicating whether the command was successful.</param>
    /// <returns>The updated <see cref="Response{T}"/> instance.</returns>
    public Response<T> WithSuccess(bool success)
    {
        Success = success;
        return this;
    }

    /// <summary>
    /// Assigns the HTTP status code to the response and returns the updated instance.
    /// </summary>
    /// <param name="statusCode">The HTTP status code for the response.</param>
    /// <returns>The updated <see cref="Response{T}"/> instance.</returns>
    public Response<T> WithStatusCode(int statusCode)
    {
        StatusCode = statusCode;
        return this;
    }

    /// <summary>
    /// Assigns a message to the response and returns the updated instance.
    /// </summary>
    /// <param name="message">The message to associate with the response.</param>
    /// <returns>The updated <see cref="Response{T}"/> instance.</returns>
    public Response<T> WithMessage(string message)
    {
        Message = message;
        return this;
    }
}