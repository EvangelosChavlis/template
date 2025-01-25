import { Pagination } from './pagination'

/**
 * Represents a response containing a list of data along with pagination information.
 * 
 * This interface is used for responses that contain a list of items along with metadata for pagination,
 * such as total number of records, current page number, and page size.
 * 
 * @template T - The type of the list of items being returned in the response.
 */
export interface ListResponse<T> {
  /**
   * The data returned by the operation, which is a list of items of type `T`.
   * This field is optional and may be null if no data is available in the response.
   */
  data?: T;

  /**
   * The pagination information related to the list of items.
   * This includes details like total records, current page, and the page size.
   */
  pagination: Pagination;
}

/**
 * Adds the given data to the provided list response.
 * 
 * This function modifies the provided `ListResponse` object to include the specified list of data.
 * 
 * @template T - The type of the data being added to the response.
 * @param response - The list response object to which data should be added.
 * @param data - The list of data to include in the response.
 * @returns The modified list response object with the added data.
 */
export function withData<T>(response: ListResponse<T>, data: T): ListResponse<T> {
  response.data = data;
  return response;
}
