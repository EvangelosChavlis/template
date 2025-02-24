// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.TerrainTypes.Dtos;

namespace server.src.Application.Geography.Natural.TerrainTypes.Interfaces;

/// <summary>
/// Defines methods for managing geography terrain types, including creation, updating, and deletion.
/// </summary>
public interface ITerrainTypeCommands
{
    /// <summary>
    /// Initializes multiple geography terrain types in bulk.
    /// </summary>
    /// <param name="dto">A list of terraintype data transfer objects to be created.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the success or failure of the bulk initialization process.</returns>
    Task<Response<string>> InitializeTerrainTypesAsync(List<CreateTerrainTypeDto> dto, 
        CancellationToken token = default);

    /// <summary>
    /// Creates a new geography terraintype.
    /// </summary>
    /// <param name="dto">The data transfer object containing terraintype details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the operation.</returns>
    Task<Response<string>> CreateTerrainTypeAsync(CreateTerrainTypeDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Updates an existing geography terraintype.
    /// </summary>
    /// <param name="id">The unique identifier of the terraintype to update.</param>
    /// <param name="dto">The data transfer object containing updated terraintype details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the update operation.</returns>
    Task<Response<string>> UpdateTerrainTypeAsync(Guid id, UpdateTerrainTypeDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Activates a terraintype by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the terraintype to activate.</param>
    /// <param name="version">The version of the terraintype for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the activation was successful.</returns>
    Task<Response<string>> ActivateTerrainTypeAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deactivates a terraintype by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the terraintype to deactivate.</param>
    /// <param name="version">The version of the terraintype for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the deactivation was successful.</returns>
    Task<Response<string>> DeactivateTerrainTypeAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deletes a geography terraintype by its unique identifier and version.
    /// </summary>
    /// <param name="id">The unique identifier of the geography terraintype to delete.</param>
    /// <param name="version">The version of the geography terraintype to ensure concurrency control during deletion.</param>
    /// <param name="token">An optional cancellation token to cancel the operation if needed.</param>
    /// <returns>A response containing the result message of the deletion operation, including success or failure details.</returns>
    Task<Response<string>> DeleteTerrainTypeAsync(Guid id, 
        Guid version, CancellationToken token = default);
}
