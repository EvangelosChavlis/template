namespace server.src.Domain.Dto.Common;

/// <summary>
/// Represents a paginated list of items, containing information about the current page, 
/// page size, total records, and total pages.
/// </summary>
public class PaginatedList
{
    /// <summary>
    /// Gets or sets the current page number in the pagination.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Gets or sets the number of items per page in the pagination.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the total number of records available across all pages.
    /// </summary>
    public int TotalRecords { get; set; }

    /// <summary>
    /// Calculates and returns the total number of pages based on the page size and total records.
    /// If the page size is zero, it returns zero to avoid division by zero.
    /// </summary>
    public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling(TotalRecords / (double)PageSize);
}
