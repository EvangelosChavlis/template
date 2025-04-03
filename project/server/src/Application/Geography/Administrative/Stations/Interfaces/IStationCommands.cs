// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Stations.Dtos;

namespace server.src.Application.Geography.Administrative.Stations.Interfaces;

/// <summary>
/// Defines methods for managing geography stations, including creation, updating, and deletion.
/// </summary>
public interface IStationCommands
{
    /// <summary>
    /// Initializes multiple geography stations in bulk.
    /// </summary>
    /// <param name="dto">A list of station data transfer objects to be created.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the success or failure of the bulk initialization process.</returns>
    Task<Response<string>> InitializeStationsAsync(List<CreateStationDto> dto, 
        CancellationToken token = default);

    /// <summary>
    /// Creates a new geography station.
    /// </summary>
    /// <param name="dto">The data transfer object containing station details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the operation.</returns>
    Task<Response<string>> CreateStationAsync(CreateStationDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Updates an existing geography station.
    /// </summary>
    /// <param name="id">The unique identifier of the station to update.</param>
    /// <param name="dto">The data transfer object containing updated station details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the update operation.</returns>
    Task<Response<string>> UpdateStationAsync(Guid id, UpdateStationDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Activates a station by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the station to activate.</param>
    /// <param name="version">The version of the station for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the activation was successful.</returns>
    Task<Response<string>> ActivateStationAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deactivates a station by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the station to deactivate.</param>
    /// <param name="version">The version of the station for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the deactivation was successful.</returns>
    Task<Response<string>> DeactivateStationAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deletes a geography station by its unique identifier and version.
    /// </summary>
    /// <param name="id">The unique identifier of the geography station to delete.</param>
    /// <param name="version">The version of the geography station to ensure concurrency control during deletion.</param>
    /// <param name="token">An optional cancellation token to cancel the operation if needed.</param>
    /// <returns>A response containing the result message of the deletion operation, including success or failure details.</returns>
    Task<Response<string>> DeleteStationAsync(Guid id, 
        Guid version, CancellationToken token = default);
}
