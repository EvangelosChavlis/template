/**
 * Represents a response containing an item of data.
 * 
 * This interface is used for responses that contain a single item of data, which may be optional.
 * 
 * @template T - The type of the data being returned in the response.
 */
export interface ItemResponse<T> {
  /**
   * The data returned by the operation, if any.
   * This field is optional and may be null if no data is available in the response.
   */
  data?: T;
}

/**
 * Adds the given data to the provided item response.
 * 
 * This function modifies the provided response object to include the specified data.
 * 
 * @template T - The type of the data being added to the response.
 * @param response - The item response object to which data should be added.
 * @param data - The data to include in the response.
 * @returns The modified item response object with the added data.
 */
export function withData<T>(response: ItemResponse<T>, data: T): ItemResponse<T> {
  response.data = data;
  return response;
}
