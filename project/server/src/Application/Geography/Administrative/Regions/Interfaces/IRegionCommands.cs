// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Regions.Dtos;

namespace server.src.Application.Geography.Administrative.Regions.Interfaces;

/// <summary>
/// Defines methods for managing geography regions, including creation, updating, and deletion.
/// </summary>
public interface IRegionCommands
{
    /// <summary>
    /// Initializes multiple geography regions in bulk.
    /// </summary>
    /// <param name="dto">A list of region data transfer objects to be created.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the success or failure of the bulk initialization process.</returns>
    Task<Response<string>> InitializeRegionsAsync(List<CreateRegionDto> dto, 
        CancellationToken token = default);

    /// <summary>
    /// Creates a new geography region.
    /// </summary>
    /// <param name="dto">The data transfer object containing region details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the operation.</returns>
    Task<Response<string>> CreateRegionAsync(CreateRegionDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Updates an existing geography region.
    /// </summary>
    /// <param name="id">The unique identifier of the region to update.</param>
    /// <param name="dto">The data transfer object containing updated region details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the update operation.</returns>
    Task<Response<string>> UpdateRegionAsync(Guid id, UpdateRegionDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Activates a region by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the region to activate.</param>
    /// <param name="version">The version of the region for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the activation was successful.</returns>
    Task<Response<string>> ActivateRegionAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deactivates a region by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the region to deactivate.</param>
    /// <param name="version">The version of the region for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the deactivation was successful.</returns>
    Task<Response<string>> DeactivateRegionAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deletes a geography region by its unique identifier and version.
    /// </summary>
    /// <param name="id">The unique identifier of the geography region to delete.</param>
    /// <param name="version">The version of the geography region to ensure concurrency control during deletion.</param>
    /// <param name="token">An optional cancellation token to cancel the operation if needed.</param>
    /// <returns>A response containing the result message of the deletion operation, including success or failure details.</returns>
    Task<Response<string>> DeleteRegionAsync(Guid id, 
        Guid version, CancellationToken token = default);
}
