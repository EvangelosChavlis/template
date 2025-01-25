namespace server.src.Domain.Dto.Common;

/// <summary>
/// Represents a response that contains a list of items with optional pagination information.
/// This class encapsulates data for a paginated list and includes success, status code, 
/// message, and pagination metadata to handle API responses more effectively.
/// </summary>
/// <typeparam name="T">The type of the items in the response.</typeparam>
public class ListResponse<T>
{   
    /// <summary>
    /// Gets or sets the list of data items associated with the response.
    /// This could be a list of any type, such as user data, product data, etc.
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Gets or sets the pagination information for the response, if applicable.
    /// This includes details like the total number of records, page size, and the current page number.
    /// </summary>
    public PaginatedList? Pagination { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the request was successful.
    /// This flag indicates the success or failure of the request processing.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the HTTP status code associated with the response.
    /// This helps to communicate the result of the API request, such as 200 for success or 404 for not found.
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Gets or sets a message providing additional context or details about the request outcome.
    /// This could be used to provide success or error messages for the client.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Sets the provided list of data to the response and returns the updated instance.
    /// This method allows for fluent chaining of the response object.
    /// </summary>
    /// <param name="data">The list of data items to associate with the response.</param>
    /// <returns>The updated <see cref="ListResponse{T}"/> instance.</returns>
    public ListResponse<T> WithData(T data)
    {
        this.Data = data;
        return this;
    }

    /// <summary>
    /// Sets the success status for the response and returns the updated instance.
    /// This method allows fluent chaining to set the success status of the response.
    /// </summary>
    /// <param name="success">Indicates whether the request was successful. This is usually a boolean flag.</param>
    /// <returns>The updated <see cref="ListResponse{T}"/> instance.</returns>
    public ListResponse<T> WithSuccess(bool success)
    {
        this.Success = success;
        return this;
    }

    /// <summary>
    /// Sets the status code for the response and returns the updated instance.
    /// This method allows fluent chaining to set the status code of the response.
    /// </summary>
    /// <param name="statusCode">The HTTP status code associated with the response, such as 200, 404, etc.</param>
    /// <returns>The updated <see cref="ListResponse{T}"/> instance.</returns>
    public ListResponse<T> WithStatusCode(int statusCode)
    {
        this.StatusCode = statusCode;
        return this;
    }

    /// <summary>
    /// Sets the message for the response and returns the updated instance.
    /// This method allows fluent chaining to set the message explaining the response.
    /// </summary>
    /// <param name="message">A message providing additional details about the request's outcome.</param>
    /// <returns>The updated <see cref="ListResponse{T}"/> instance.</returns>
    public ListResponse<T> WithMessage(string message)
    {
        this.Message = message;
        return this;
    }
}