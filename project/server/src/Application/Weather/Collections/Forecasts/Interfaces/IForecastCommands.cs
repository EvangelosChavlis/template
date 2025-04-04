// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Weather.Collections.Forecasts.Dtos;

namespace server.src.Application.Weather.Collections.Forecasts.Interfaces;

/// <summary>
/// Defines methods for managing weather forecasts, including initialization, creation, updating, and deletion.
/// </summary>
public interface IForecastCommands
{
    /// <summary>
    /// Initializes multiple weather forecasts in bulk.
    /// </summary>
    /// <param name="dto">A list of forecast data transfer objects to be created.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the success or failure of the bulk initialization process.</returns>
    Task<Response<string>> InitializeForecastsAsync(List<CreateForecastDto> dto, 
        CancellationToken token = default);

    /// <summary>
    /// Creates a new weather forecast.
    /// </summary>
    /// <param name="dto">The data transfer object containing forecast details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the operation.</returns>
    Task<Response<string>> CreateForecastAsync(CreateForecastDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Updates an existing weather forecast.
    /// </summary>
    /// <param name="id">The unique identifier of the forecast to update.</param>
    /// <param name="dto">The data transfer object containing updated forecast details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the update operation.</returns>
    Task<Response<string>> UpdateForecastAsync(Guid id, UpdateForecastDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Deletes a weather forecast by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the forecast to delete.</param>
    /// <param name="version">The version of the role for concurrency control.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the deletion operation.</returns>
    Task<Response<string>> DeleteForecastAsync(Guid id, 
        Guid version, CancellationToken token = default);
}
