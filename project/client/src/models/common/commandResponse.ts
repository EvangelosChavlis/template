/**
 * Represents a command response.
 * 
 * This interface is used to represent the result of a command or operation, including a success flag and optional data.
 * 
 * @template T - The type of the data being returned in the response.
 */
export interface CommandResponse<T> {
    /**
     * The data returned by the command, if any.
     * This is optional and may be null if no data is returned with the response.
     */
    data?: T;
  
    /**
     * A boolean indicating whether the command was successful.
     * True if the command succeeded, false otherwise.
     */
    success: boolean;
  }
  
  /**
   * Adds the given data to the provided command response.
   * 
   * This function modifies the provided response object to include the specified data.
   * 
   * @template T - The type of the data being added to the response.
   * @param response - The command response object to which data should be added.
   * @param data - The data to include in the response.
   * @returns The modified command response object with the added data.
   */
  export function withData<T>(response: CommandResponse<T>, data: T): CommandResponse<T> {
    response.data = data;
    return response;
  }
  
  /**
   * Sets the success flag of the provided command response.
   * 
   * This function modifies the provided response object to indicate whether the command was successful.
   * 
   * @template T - The type of the data in the response.
   * @param response - The command response object to which the success flag should be applied.
   * @param success - A boolean indicating whether the command was successful.
   * @returns The modified command response object with the updated success flag.
   */
  export function withSuccess<T>(response: CommandResponse<T>, success: boolean): CommandResponse<T> {
    response.success = success;
    return response;
  }
  