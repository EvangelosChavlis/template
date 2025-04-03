// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.SurfaceTypes.Dtos;

namespace server.src.Application.Geography.Natural.SurfaceTypes.Interfaces;

/// <summary>
/// Defines methods for managing geography surface types, including creation, updating, and deletion.
/// </summary>
public interface ISurfaceTypeCommands
{
    /// <summary>
    /// Initializes multiple geography surface types in bulk.
    /// </summary>
    /// <param name="dto">A list of SurfaceType data transfer objects to be created.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the success or failure of the bulk initialization process.</returns>
    Task<Response<string>> InitializeSurfaceTypesAsync(List<CreateSurfaceTypeDto> dto, 
        CancellationToken token = default);

    /// <summary>
    /// Creates a new geography SurfaceType.
    /// </summary>
    /// <param name="dto">The data transfer object containing SurfaceType details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the operation.</returns>
    Task<Response<string>> CreateSurfaceTypeAsync(CreateSurfaceTypeDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Updates an existing geography SurfaceType.
    /// </summary>
    /// <param name="id">The unique identifier of the SurfaceType to update.</param>
    /// <param name="dto">The data transfer object containing updated SurfaceType details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the update operation.</returns>
    Task<Response<string>> UpdateSurfaceTypeAsync(Guid id, UpdateSurfaceTypeDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Activates a SurfaceType by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the SurfaceType to activate.</param>
    /// <param name="version">The version of the SurfaceType for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the activation was successful.</returns>
    Task<Response<string>> ActivateSurfaceTypeAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deactivates a SurfaceType by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the SurfaceType to deactivate.</param>
    /// <param name="version">The version of the SurfaceType for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the deactivation was successful.</returns>
    Task<Response<string>> DeactivateSurfaceTypeAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deletes a geography SurfaceType by its unique identifier and version.
    /// </summary>
    /// <param name="id">The unique identifier of the geography SurfaceType to delete.</param>
    /// <param name="version">The version of the geography SurfaceType to ensure concurrency control during deletion.</param>
    /// <param name="token">An optional cancellation token to cancel the operation if needed.</param>
    /// <returns>A response containing the result message of the deletion operation, including success or failure details.</returns>
    Task<Response<string>> DeleteSurfaceTypeAsync(Guid id, 
        Guid version, CancellationToken token = default);
}
