namespace server.src.Domain.Models.Common;

/// <summary>
/// Represents a wrapper for a collection of items with pagination and query information.
/// This class is typically used to encapsulate data rows and related metadata such as pagination
/// and query parameters for API responses.
/// </summary>
/// <typeparam name="T">The type of data being encapsulated (e.g., a list of items).</typeparam>
public class Envelope<T>
{
    /// <summary>
    /// Gets or sets the collection of data rows (items) of type <typeparamref name="T"/>.
    /// </summary>
    public IList<T> Rows { get; set; }

    /// <summary>
    /// Gets or sets the query metadata such as pagination and sorting information.
    /// Contains details like the page number, page size, and total records.
    /// </summary>
    public UrlQuery UrlQuery { get; set; }
}