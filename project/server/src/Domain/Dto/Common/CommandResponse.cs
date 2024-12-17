namespace server.src.Domain.Dto.Common;

/// <summary>
/// Represents a standard response for commands that perform an action and may return data.
/// </summary>
/// <typeparam name="T">The type of data associated with the response.</typeparam>
public class CommandResponse<T>
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
    /// Assigns the provided data to the response and returns the updated instance.
    /// </summary>
    /// <param name="data">The data to associate with the response.</param>
    /// <returns>The updated <see cref="CommandResponse{T}"/> instance.</returns>
    public CommandResponse<T> WithData(T data)
    {
        Data = data;
        return this;
    }

    /// <summary>
    /// Assigns the success status to the response and returns the updated instance.
    /// </summary>
    /// <param name="success">A value indicating whether the command was successful.</param>
    /// <returns>The updated <see cref="CommandResponse{T}"/> instance.</returns>
    public CommandResponse<T> WithSuccess(bool success)
    {
        Success = success;
        return this;
    }
}
