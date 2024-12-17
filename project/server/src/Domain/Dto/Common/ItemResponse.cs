namespace server.src.Domain.Dto.Common;

/// <summary>
/// Represents a response that contains a single item of data.
/// </summary>
/// <typeparam name="T">The type of the item in the response.</typeparam>
public class ItemResponse<T>
{
    /// <summary>
    /// Gets or sets the data associated with the response.
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Sets the provided data to the response and returns the updated instance.
    /// </summary>
    /// <param name="data">The data to associate with the response.</param>
    /// <returns>The updated <see cref="ItemResponse{T}"/> instance.</returns>
    public ItemResponse<T> WithData(T data)
    {
        this.Data = data;
        return this;
    }
}
