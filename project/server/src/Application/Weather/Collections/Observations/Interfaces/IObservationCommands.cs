// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Weather.Collections.Observations.Dtos;

namespace server.src.Application.Weather.Collections.Observations.Interfaces;

/// <summary>
/// Defines methods for managing weather observations, including initialization, creation, updating, and deletion.
/// </summary>
public interface IObservationCommands
{
    /// <summary>
    /// Initializes multiple weather observations in bulk.
    /// </summary>
    /// <param name="dto">A list of observation data transfer objects to be created.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the success or failure of the bulk initialization process.</returns>
    Task<Response<string>> InitializeObservationsAsync(List<CreateObservationDto> dto, 
        CancellationToken token = default);

    /// <summary>
    /// Creates a new weather observation.
    /// </summary>
    /// <param name="dto">The data transfer object containing observation details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the operation.</returns>
    Task<Response<string>> CreateObservationAsync(CreateObservationDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Updates an existing weather observation.
    /// </summary>
    /// <param name="id">The unique identifier of the observation to update.</param>
    /// <param name="dto">The data transfer object containing updated observation details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the update operation.</returns>
    Task<Response<string>> UpdateObservationAsync(Guid id, UpdateObservationDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Deletes a weather observation by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the observation to delete.</param>
    /// <param name="version">The version of the role for concurrency control.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the deletion operation.</returns>
    Task<Response<string>> DeleteObservationAsync(Guid id, 
        Guid version, CancellationToken token = default);
}
