// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Continents.Dtos;

namespace server.src.Application.Geography.Administrative.Continents.Interfaces;

/// <summary>
/// Defines methods for managing geography continents, including creation, updating, and deletion.
/// </summary>
public interface IContinentCommands
{
    /// <summary>
    /// Initializes multiple geography continents in bulk.
    /// </summary>
    /// <param name="dto">A list of continent data transfer objects to be created.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the success or failure of the bulk initialization process.</returns>
    Task<Response<string>> InitializeContinentsAsync(List<CreateContinentDto> dto, 
        CancellationToken token = default);

    /// <summary>
    /// Creates a new geography continent.
    /// </summary>
    /// <param name="dto">The data transfer object containing continent details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the operation.</returns>
    Task<Response<string>> CreateContinentAsync(CreateContinentDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Updates an existing geography continent.
    /// </summary>
    /// <param name="id">The unique identifier of the continent to update.</param>
    /// <param name="dto">The data transfer object containing updated continent details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the update operation.</returns>
    Task<Response<string>> UpdateContinentAsync(Guid id, UpdateContinentDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Activates a continent by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the continent to activate.</param>
    /// <param name="version">The version of the continent for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the activation was successful.</returns>
    Task<Response<string>> ActivateContinentAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deactivates a continent by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the continent to deactivate.</param>
    /// <param name="version">The version of the continent for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the deactivation was successful.</returns>
    Task<Response<string>> DeactivateContinentAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deletes a geography continent by its unique identifier and version.
    /// </summary>
    /// <param name="id">The unique identifier of the geography continent to delete.</param>
    /// <param name="version">The version of the geography continent to ensure concurrency control during deletion.</param>
    /// <param name="token">An optional cancellation token to cancel the operation if needed.</param>
    /// <returns>A response containing the result message of the deletion operation, including success or failure details.</returns>
    Task<Response<string>> DeleteContinentAsync(Guid id, 
        Guid version, CancellationToken token = default);
}
