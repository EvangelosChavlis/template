// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;

namespace server.src.Application.Geography.Administrative.Neighborhoods.Interfaces;

/// <summary>
/// Defines methods for managing geography neighborhoods, including creation, updating, and deletion.
/// </summary>
public interface INeighborhoodCommands
{
    /// <summary>
    /// Initializes multiple geography neighborhoods in bulk.
    /// </summary>
    /// <param name="dto">A list of neighborhood data transfer objects to be created.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the success or failure of the bulk initialization process.</returns>
    Task<Response<string>> InitializeNeighborhoodsAsync(List<CreateNeighborhoodDto> dto, 
        CancellationToken token = default);

    /// <summary>
    /// Creates a new geography neighborhood.
    /// </summary>
    /// <param name="dto">The data transfer object containing neighborhood details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the operation.</returns>
    Task<Response<string>> CreateNeighborhoodAsync(CreateNeighborhoodDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Updates an existing geography neighborhood.
    /// </summary>
    /// <param name="id">The unique identifier of the neighborhood to update.</param>
    /// <param name="dto">The data transfer object containing updated neighborhood details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the update operation.</returns>
    Task<Response<string>> UpdateNeighborhoodAsync(Guid id, UpdateNeighborhoodDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Activates a neighborhood by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the neighborhood to activate.</param>
    /// <param name="version">The version of the neighborhood for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the activation was successful.</returns>
    Task<Response<string>> ActivateNeighborhoodAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deactivates a neighborhood by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the neighborhood to deactivate.</param>
    /// <param name="version">The version of the neighborhood for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the deactivation was successful.</returns>
    Task<Response<string>> DeactivateNeighborhoodAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deletes a geography neighborhood by its unique identifier and version.
    /// </summary>
    /// <param name="id">The unique identifier of the geography neighborhood to delete.</param>
    /// <param name="version">The version of the geography neighborhood to ensure concurrency control during deletion.</param>
    /// <param name="token">An optional cancellation token to cancel the operation if needed.</param>
    /// <returns>A response containing the result message of the deletion operation, including success or failure details.</returns>
    Task<Response<string>> DeleteNeighborhoodAsync(Guid id, 
        Guid version, CancellationToken token = default);
}
