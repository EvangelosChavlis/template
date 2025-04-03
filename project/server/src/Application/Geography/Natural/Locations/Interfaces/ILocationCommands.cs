// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.Locations.Dtos;

namespace server.src.Application.Geography.Natural.Locations.Interfaces;

/// <summary>
/// Defines methods for managing geography surface types, including creation, updating, and deletion.
/// </summary>
public interface ILocationCommands
{
    /// <summary>
    /// Initializes multiple geography locations in bulk.
    /// </summary>
    /// <param name="dto">A list of surface type data transfer objects to be created.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the success or failure of the bulk initialization process.</returns>
    Task<Response<string>> InitializeLocationsAsync(List<CreateLocationDto> dto, 
        CancellationToken token = default);

    /// <summary>
    /// Creates a new geography location.
    /// </summary>
    /// <param name="dto">The data transfer object containing surface type details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the operation.</returns>
    Task<Response<string>> CreateLocationAsync(CreateLocationDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Updates an existing geography surface type.
    /// </summary>
    /// <param name="id">The unique identifier of the surface type to update.</param>
    /// <param name="dto">The data transfer object containing updated surface type details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the update operation.</returns>
    Task<Response<string>> UpdateLocationAsync(Guid id, UpdateLocationDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Activates a surface type by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the surface type to activate.</param>
    /// <param name="version">The version of the surface type for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the activation was successful.</returns>
    Task<Response<string>> ActivateLocationAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deactivates a surface type by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the surface type to deactivate.</param>
    /// <param name="version">The version of the surface type for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the deactivation was successful.</returns>
    Task<Response<string>> DeactivateLocationAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deletes a geography surface type by its unique identifier and version.
    /// </summary>
    /// <param name="id">The unique identifier of the geography surface type to delete.</param>
    /// <param name="version">The version of the geography surface type to ensure concurrency control during deletion.</param>
    /// <param name="token">An optional cancellation token to cancel the operation if needed.</param>
    /// <returns>A response containing the result message of the deletion operation, including success or failure details.</returns>
    Task<Response<string>> DeleteLocationAsync(Guid id, 
        Guid version, CancellationToken token = default);
}
