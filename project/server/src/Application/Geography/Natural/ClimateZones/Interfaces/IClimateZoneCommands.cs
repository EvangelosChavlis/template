// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.ClimateZones.Dtos;

namespace server.src.Application.Geography.Natural.ClimateZones.Interfaces;

/// <summary>
/// Defines methods for managing geography climate zones, including creation, updating, and deletion.
/// </summary>
public interface IClimateZoneCommands
{
    /// <summary>
    /// Initializes multiple geography climate zones in bulk.
    /// </summary>
    /// <param name="dto">A list of terrain type data transfer objects to be created.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the success or failure of the bulk initialization process.</returns>
    Task<Response<string>> InitializeClimateZonesAsync(List<CreateClimateZoneDto> dto, 
        CancellationToken token = default);

    /// <summary>
    /// Creates a new geography climatezone.
    /// </summary>
    /// <param name="dto">The data transfer object containing terrain type details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the operation.</returns>
    Task<Response<string>> CreateClimateZoneAsync(CreateClimateZoneDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Updates an existing geography terrain type.
    /// </summary>
    /// <param name="id">The unique identifier of the terrain type to update.</param>
    /// <param name="dto">The data transfer object containing updated terrain type details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the update operation.</returns>
    Task<Response<string>> UpdateClimateZoneAsync(Guid id, UpdateClimateZoneDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Activates a terrain type by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the terrain type to activate.</param>
    /// <param name="version">The version of the terrain type for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the activation was successful.</returns>
    Task<Response<string>> ActivateClimateZoneAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deactivates a terrain type by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the terrain type to deactivate.</param>
    /// <param name="version">The version of the terrain type for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the deactivation was successful.</returns>
    Task<Response<string>> DeactivateClimateZoneAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deletes a geography terrain type by its unique identifier and version.
    /// </summary>
    /// <param name="id">The unique identifier of the geography terrain type to delete.</param>
    /// <param name="version">The version of the geography terrain type to ensure concurrency control during deletion.</param>
    /// <param name="token">An optional cancellation token to cancel the operation if needed.</param>
    /// <returns>A response containing the result message of the deletion operation, including success or failure details.</returns>
    Task<Response<string>> DeleteClimateZoneAsync(Guid id, 
        Guid version, CancellationToken token = default);
}
