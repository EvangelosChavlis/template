/**
 * Represents a basic error record for a list of errors.
 * 
 * This interface includes essential details about an error, such as the error message, status code,
 * and the timestamp when the error occurred.
 */
export interface ListItemErrorDto {
    /**
     * The unique identifier for the error record.
     */
    id: string;
  
    /**
     * The error message describing the nature of the error.
     */
    error: string;
  
    /**
     * The HTTP status code associated with the error (e.g., "500" for server errors, "404" for not found).
     */
    statusCode: number;
  
    /**
     * The timestamp when the error was recorded or occurred.
     */
    timestamp: string;
  }
  
  /**
   * Represents detailed error information for a single error record.
   * 
   * This interface contains extended details, including the instance of the application where the error occurred,
   * the exception type, stack trace, and the timestamp of the error.
   */
  export interface ItemErrorDto {
    /**
     * The unique identifier for the error record.
     */
    id: string;
  
    /**
     * The error message describing the nature of the error.
     */
    error: string;
  
    /**
     * The HTTP status code associated with the error (e.g., "500" for server errors, "404" for not found).
     */
    statusCode: number;
  
    /**
     * The instance of the application where the error occurred, useful in multi-instance environments.
     */
    instance: string;
  
    /**
     * The type of the exception that was thrown, such as "TypeError", "SyntaxError", etc.
     */
    exceptionType: string;
  
    /**
     * The stack trace of the error, providing detailed information for debugging.
     */
    stackTrace: string;
  
    /**
     * The timestamp when the error occurred.
     */
    timestamp: string;
  }
  