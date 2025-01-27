// source
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Metrics;
using server.src.Domain.Models.Common;

namespace server.src.Application.Auth.Telemetry.Interfaces;

/// <summary>
/// Defines methods for retrieving Telemetry-related data, including lists, details, and selection options.
/// </summary>
public interface ITelemetryQueries
{
    /// <summary>
    /// Retrieves a paginated list of Telemetry based on query parameters.
    /// </summary>
    /// <param name="urlQuery">Query parameters for filtering and pagination.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A list response containing Telemetry.</returns>
    Task<ListResponse<List<ListItemTelemetryDto>>> GetTelemetryAsync(UrlQuery urlQuery,
        CancellationToken token = default);

    /// <summary>
    /// Retrieves a paginated list of Telemetry assigned to a specific user.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="urlQuery">Query parameters for filtering and pagination.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A list response containing Telemetry assigned to the specified user.</returns>
    Task<ListResponse<List<ListItemTelemetryDto>>> GetTelemetryByUserIdAsync(Guid id, UrlQuery urlQuery,
        CancellationToken token = default);

    /// <summary>
    /// Retrieves detailed information about a specific Telemetry by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Telemetry.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing detailed Telemetry information.</returns>
    Task<Response<ItemTelemetryDto>> GetTelemetryByIdAsync(Guid id,
        CancellationToken token = default);
}