namespace server.src.Domain.Models.Common;

/// <summary>
/// Represents query parameters for paginated data retrieval.
/// This class is used to encapsulate filter, sorting, and pagination options
/// when fetching data from an API or database.
/// </summary>
public class UrlQuery
{
    /// <summary>
    /// Gets or sets the page number for paginated data.
    /// Default is set to 1 if not provided.
    /// </summary>
    public int? PageNumber { get; set; } = 1;

    /// <summary>
    /// Gets or sets the page size (number of records per page) for paginated data.
    /// Default is set to 20 if not provided.
    /// </summary>
    public int PageSize { get; set; } = 20;

    /// <summary>
    /// Gets or sets the filter query to be applied to the data.
    /// This property is used to filter the data based on specific conditions.
    /// </summary>
    public string? Filter { get; set; }

    /// <summary>
    /// Gets or sets the field by which the data should be sorted.
    /// </summary>
    public string? SortBy { get; set; }

    /// <summary>
    /// Gets or sets a boolean indicating whether the sorting should be in descending order.
    /// Default is false, meaning the sorting is in ascending order.
    /// </summary>
    public bool SortDescending { get; set; }

    /// <summary>
    /// Returns true if the Filter property is not null or empty.
    /// Used to determine if a filter is applied in the query.
    /// </summary>
    public bool HasFilter => !string.IsNullOrEmpty(Filter);

    /// <summary>
    /// Returns true if the SortBy property is not null or empty.
    /// Used to determine if sorting is applied in the query.
    /// </summary>
    public bool HasSortBy => !string.IsNullOrEmpty(SortBy);

    /// <summary>
    /// Gets or sets the total number of records in the data set.
    /// This property is used for pagination purposes to determine the total number of pages.
    /// </summary>
    public int TotalRecords { get; set; }
}