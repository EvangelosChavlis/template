// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Collections.Forecasts.Dtos;

namespace server.src.Application.Weather.Collections.Forecasts.Interfaces;

/// <summary>
/// Interface for handling weather forecast queries.
/// </summary>
public interface IForecastQueries
{
    /// <summary>
    /// Retrieves statistical data related to weather forecasts.
    /// </summary>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing a list of forecast statistics.</returns>
    Task<Response<List<StatItemForecastDto>>> GetForecastsStatsAsync(CancellationToken token = default);

    /// <summary>
    /// Retrieves a paginated list of weather forecasts based on query parameters.
    /// </summary>
    /// <param name="urlQuery">Query parameters for filtering and pagination.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A list response containing weather forecasts.</returns>
    Task<ListResponse<List<ListItemForecastDto>>> GetForecastsAsync(UrlQuery urlQuery, 
        CancellationToken token = default);

    /// <summary>
    /// Retrieves detailed forecast data for a specific forecast identified by its unique ID.
    /// </summary>
    /// <param name="id">The unique identifier of the forecast.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing forecast details.</returns>
    Task<Response<ItemForecastDto>> GetForecastByIdAsync(Guid id, 
        CancellationToken token = default);
}
