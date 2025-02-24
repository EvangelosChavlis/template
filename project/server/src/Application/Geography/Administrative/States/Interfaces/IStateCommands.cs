// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.States.Dtos;

namespace server.src.Application.Geography.Administrative.States.Interfaces;

/// <summary>
/// Defines methods for managing geography states, including creation, updating, and deletion.
/// </summary>
public interface IStateCommands
{
    /// <summary>
    /// Initializes multiple geography states in bulk.
    /// </summary>
    /// <param name="dto">A list of state data transfer objects to be created.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the success or failure of the bulk initialization process.</returns>
    Task<Response<string>> InitializeStatesAsync(List<CreateStateDto> dto, 
        CancellationToken token = default);

    /// <summary>
    /// Creates a new geography state.
    /// </summary>
    /// <param name="dto">The data transfer object containing state details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the operation.</returns>
    Task<Response<string>> CreateStateAsync(CreateStateDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Updates an existing geography state.
    /// </summary>
    /// <param name="id">The unique identifier of the state to update.</param>
    /// <param name="dto">The data transfer object containing updated state details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the update operation.</returns>
    Task<Response<string>> UpdateStateAsync(Guid id, UpdateStateDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Activates a state by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the state to activate.</param>
    /// <param name="version">The version of the state for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the activation was successful.</returns>
    Task<Response<string>> ActivateStateAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deactivates a state by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the state to deactivate.</param>
    /// <param name="version">The version of the state for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the deactivation was successful.</returns>
    Task<Response<string>> DeactivateStateAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deletes a geography state by its unique identifier and version.
    /// </summary>
    /// <param name="id">The unique identifier of the geography state to delete.</param>
    /// <param name="version">The version of the geography state to ensure concurrency control during deletion.</param>
    /// <param name="token">An optional cancellation token to cancel the operation if needed.</param>
    /// <returns>A response containing the result message of the deletion operation, including success or failure details.</returns>
    Task<Response<string>> DeleteStateAsync(Guid id, 
        Guid version, CancellationToken token = default);
}
