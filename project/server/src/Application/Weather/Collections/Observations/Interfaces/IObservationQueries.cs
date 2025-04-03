// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Collections.Observations.Dtos;

namespace server.src.Application.Weather.Collections.Observations.Interfaces;

/// <summary>
/// Interface for handling weather observation queries.
/// </summary>
public interface IObservationQueries
{
    /// <summary>
    /// Retrieves statistical data related to weather observations.
    /// </summary>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing a list of observation statistics.</returns>
    Task<Response<List<StatItemObservationDto>>> GetObservationsStatsAsync(CancellationToken token = default);

    /// <summary>
    /// Retrieves a paginated list of weather observations based on query parameters.
    /// </summary>
    /// <param name="urlQuery">Query parameters for filtering and pagination.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A list response containing weather observations.</returns>
    Task<ListResponse<List<ListItemObservationDto>>> GetObservationsAsync(UrlQuery urlQuery, 
        CancellationToken token = default);

    /// <summary>
    /// Retrieves detailed observation data for a specific observation identified by its unique ID.
    /// </summary>
    /// <param name="id">The unique identifier of the observation.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing observation details.</returns>
    Task<Response<ItemObservationDto>> GetObservationByIdAsync(Guid id, 
        CancellationToken token = default);
}
