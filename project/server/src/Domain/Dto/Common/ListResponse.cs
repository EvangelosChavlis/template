namespace server.src.Domain.Dto.Common;

/// <summary>
/// Represents a response that contains a list of items with optional pagination information.
/// </summary>
/// <typeparam name="T">The type of the items in the response.</typeparam>
public class ListResponse<T>
{
    /// <summary>
    /// Gets or sets the list of data items associated with the response.
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Gets or sets the pagination information for the response, if applicable.
    /// </summary>
    public PaginatedList? Pagination { get; set; }

    /// <summary>
    /// Sets the provided list of data to the response and returns the updated instance.
    /// </summary>
    /// <param name="data">The list of data items to associate with the response.</param>
    /// <returns>The updated <see cref="ListResponse{T}"/> instance.</returns>
    public ListResponse<T> WithData(T data)
    {
        this.Data = data;
        return this;
    }
}
