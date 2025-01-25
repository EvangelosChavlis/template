/**
 * Represents a list item for telemetry data.
 * 
 * This interface includes basic details of a telemetry record such as the method, path, status code,
 * response time, and the timestamp when the request was made.
 */
export interface ListItemTelemetryDto {
  /**
   * The unique identifier for the telemetry record.
   */
  id: string;

  /**
   * The HTTP method used for the request (e.g., "GET", "POST").
   */
  method: string;

  /**
   * The request path or endpoint being accessed.
   */
  path: string;

  /**
   * The HTTP status code returned from the request (e.g., "200", "404").
   */
  statusCode: string;

  /**
   * The time taken to process the request, measured in milliseconds.
   */
  responseTime: number;

  /**
   * The timestamp of the request when it was received by the server.
   */
  requestTimestamp: string;
}
  
/**
 * Represents detailed telemetry data for a single request.
 * 
 * This interface includes extended information such as resource usage (CPU, memory), request and response body sizes, 
 * response time, and additional metadata for analysis.
 */
export interface ItemTelemetryDto {
  /**
   * The unique identifier for the telemetry record.
   */
  id: string;

  /**
   * The HTTP method used for the request (e.g., "GET", "POST").
   */
  method: string;

  /**
   * The request path or endpoint being accessed.
   */
  path: string;

  /**
   * The HTTP status code returned from the request (e.g., "200", "404").
   */
  statusCode: string;

  /**
   * The time taken to process the request, measured in milliseconds.
   */
  responseTime: number;

  /**
   * The amount of memory used during the request processing, typically measured in bytes.
   */
  memoryUsed: number;

  /**
   * The CPU usage during the request processing, typically measured as a percentage.
   */
  cpUusage: number;

  /**
   * The size of the request body, typically measured in bytes.
   */
  requestBodySize: number;

  /**
   * The timestamp when the request was received by the server.
   */
  requestTimestamp: string;

  /**
   * The size of the response body, typically measured in bytes.
   */
  responseBodySize: number;

  /**
   * The timestamp when the response was sent back to the client.
   */
  responseTimestamp: string;

  /**
   * The IP address of the client making the request.
   */
  clientIp: string;

  /**
   * The user agent string sent by the client, typically containing information about the client’s browser or device.
   */
  userAgent: string;

  /**
   * The thread ID on the server processing the request, useful for diagnosing concurrency issues.
   */
  threadId: string;

  /**
   * User ID associated with the telemetry record.
   */
  userId: string;

    /**
    * Username associated with the telemetry record.
    */
  userName: string;
}
  