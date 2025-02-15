// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Weather.MoonPhases.Dtos;

namespace server.src.Application.Weather.MoonPhases.Interfaces;

/// <summary>
/// Defines methods for managing weather moon phases, including creation, updating, and deletion.
/// </summary>
public interface IMoonPhaseCommands
{
    /// <summary>
    /// Initializes multiple weather moon phases in bulk.
    /// </summary>
    /// <param name="dto">A list of moonphase data transfer objects to be created.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the success or failure of the bulk initialization process.</returns>
    Task<Response<string>> InitializeMoonPhasesAsync(List<CreateMoonPhaseDto> dto, 
        CancellationToken token = default);

    /// <summary>
    /// Creates a new weather moonphase.
    /// </summary>
    /// <param name="dto">The data transfer object containing moon phase details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the operation.</returns>
    Task<Response<string>> CreateMoonPhaseAsync(CreateMoonPhaseDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Updates an existing weather moonphase.
    /// </summary>
    /// <param name="id">The unique identifier of the moon phase to update.</param>
    /// <param name="dto">The data transfer object containing updated moon phase details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the update operation.</returns>
    Task<Response<string>> UpdateMoonPhaseAsync(Guid id, UpdateMoonPhaseDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Activates a moonphase by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the moon phase to activate.</param>
    /// <param name="version">The version of the moonphase for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the activation was successful.</returns>
    Task<Response<string>> ActivateMoonPhaseAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deactivates a moonphase by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the moon phase to deactivate.</param>
    /// <param name="version">The version of the moon phase for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the deactivation was successful.</returns>
    Task<Response<string>> DeactivateMoonPhaseAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deletes a weather moonphase by its unique identifier and version.
    /// </summary>
    /// <param name="id">The unique identifier of the weather moon phase to delete.</param>
    /// <param name="version">The version of the weather moon phase to ensure concurrency control during deletion.</param>
    /// <param name="token">An optional cancellation token to cancel the operation if needed.</param>
    /// <returns>A response containing the result message of the deletion operation, including success or failure details.</returns>
    Task<Response<string>> DeleteMoonPhaseAsync(Guid id, 
        Guid version, CancellationToken token = default);
}
