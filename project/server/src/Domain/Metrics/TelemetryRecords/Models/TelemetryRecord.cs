// source
using server.src.Domain.Auth.Users.Models;
using server.src.Domain.Common.Models;
using server.src.Domain.Metrics.AuditLogs.Models;
using server.src.Domain.Metrics.Stories;

namespace server.src.Domain.Metrics.TelemetryRecords.Models;
/// <summary>
/// Represents telemetry data for an HTTP request and its associated metrics.
/// This class stores performance metrics related to the request and response lifecycle, 
/// such as request method, status code, response time, memory usage, etc.
/// </summary>
public class TelemetryRecord : BaseEntity
{
    /// <summary>
    /// Gets or sets the HTTP method used for the request (e.g., GET, POST, PUT, DELETE).
    /// This represents the type of operation performed on the server.
    /// </summary>
    public string Method { get; set; }

    /// <summary>
    /// Gets or sets the path of the requested resource (e.g., "/api/users").
    /// This represents the URL path accessed by the client.
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// Gets or sets the HTTP status code returned by the server (e.g., 200, 404, 500).
    /// This status code indicates the result of the request.
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Gets or sets the response time in milliseconds.
    /// This indicates how long it took for the server to process the request and send a response.
    /// </summary>
    public long ResponseTime { get; set; }

    /// <summary>
    /// Gets or sets the amount of memory (in bytes) used during the request processing.
    /// This helps monitor memory consumption during the handling of a request.
    /// </summary>
    public long MemoryUsed { get; set; }

    /// <summary>
    /// Gets or sets the CPU usage (as a percentage) during the request processing.
    /// This indicates how much of the server's CPU resources were used to handle the request.
    /// </summary>
    public double CPUusage { get; set; }

    /// <summary>
    /// Gets or sets the size of the request body in bytes.
    /// This represents the size of data sent by the client in the request (e.g., JSON payload).
    /// </summary>
    public long RequestBodySize { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of when the request was received by the server.
    /// This marks the exact time the request entered the server for processing.
    /// </summary>
    public DateTime RequestTimestamp { get; set; }

    /// <summary>
    /// Gets or sets the size of the response body in bytes.
    /// This represents the size of the data returned by the server to the client.
    /// </summary>
    public long ResponseBodySize { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of when the response was sent back to the client.
    /// This marks the exact time the response was generated and sent.
    /// </summary>
    public DateTime ResponseTimestamp { get; set; }

    /// <summary>
    /// Gets or sets the IP address of the client making the request.
    /// This represents the source IP of the client that initiated the request.
    /// </summary>
    public string ClientIp { get; set; }

    /// <summary>
    /// Gets or sets the user agent string sent by the client.
    /// The user agent string typically contains information about the client’s browser, operating system, etc.
    /// </summary>
    public string UserAgent { get; set; }

    /// <summary>
    /// Gets or sets the thread identifier that processed the request.
    /// This is useful for debugging and tracing the request processing on the server.
    /// </summary>
    public string ThreadId { get; set; }

    #region Foreign keys

    /// <summary>
    /// Gets or sets the ID of the user associated with the telemetry record.
    /// This establishes a relationship between the telemetry data and a user.
    /// </summary>
    public Guid UserId { get; set; }
    
    #endregion

    #region Navigation properties

    /// <summary>
    /// Gets or sets the user associated with the telemetry record.
    /// This represents the relationship between telemetry data and the user.
    /// </summary>
    public virtual User User { get; set; }

    /// <summary>
    /// Gets or sets the audit log associated with this telemetry record.
    /// </summary>
    public virtual List<AuditLog> AuditLogs { get; set; }

    /// <summary>
    /// Gets or sets the history entity associated with this telemetry record.
    /// </summary>
    public virtual List<Story> Stories { get; set; }
    
    #endregion
}