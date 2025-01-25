/**
 * Represents the pagination details for a list of data.
 * 
 * This interface contains information about the current page, the number of items per page, 
 * the total number of records, and the total number of pages available.
 * It is typically used in scenarios where data is returned in paginated form, 
 * providing both the data and metadata for navigation purposes.
 */
export interface Pagination {
  /**
   * The current page number being viewed.
   * This is a 1-based index, meaning the first page is numbered 1.
   */
  pageNumber: number;

  /**
   * The number of items per page.
   * This defines how many items will be included in each page of the response.
   */
  pageSize: number;

  /**
   * The total number of records available across all pages.
   * This represents the full count of items that match the query criteria, not limited by the page size.
   */
  totalRecords: number;

  /**
   * The total number of pages available for the data set.
   * This is calculated based on the total number of records and the page size.
   */
  totalPages: number;
}
