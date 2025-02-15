// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Weather.Warnings.Dtos;

namespace server.src.Application.Weather.Warnings.Interfaces;

/// <summary>
/// Defines methods for managing weather warnings, including creation, updating, and deletion.
/// </summary>
public interface IWarningCommands
{
    /// <summary>
    /// Initializes multiple weather warnings in bulk.
    /// </summary>
    /// <param name="dto">A list of warning data transfer objects to be created.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the success or failure of the bulk initialization process.</returns>
    Task<Response<string>> InitializeWarningsAsync(List<CreateWarningDto> dto, 
        CancellationToken token = default);

    /// <summary>
    /// Creates a new weather warning.
    /// </summary>
    /// <param name="dto">The data transfer object containing warning details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the operation.</returns>
    Task<Response<string>> CreateWarningAsync(CreateWarningDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Updates an existing weather warning.
    /// </summary>
    /// <param name="id">The unique identifier of the warning to update.</param>
    /// <param name="dto">The data transfer object containing updated warning details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the update operation.</returns>
    Task<Response<string>> UpdateWarningAsync(Guid id, UpdateWarningDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Activates a warning by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the warning to activate.</param>
    /// <param name="version">The version of the warning for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the activation was successful.</returns>
    Task<Response<string>> ActivateWarningAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deactivates a warning by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the warning to deactivate.</param>
    /// <param name="version">The version of the warning for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the deactivation was successful.</returns>
    Task<Response<string>> DeactivateWarningAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deletes a weather warning by its unique identifier and version.
    /// </summary>
    /// <param name="id">The unique identifier of the weather warning to delete.</param>
    /// <param name="version">The version of the weather warning to ensure concurrency control during deletion.</param>
    /// <param name="token">An optional cancellation token to cancel the operation if needed.</param>
    /// <returns>A response containing the result message of the deletion operation, including success or failure details.</returns>
    Task<Response<string>> DeleteWarningAsync(Guid id, 
        Guid version, CancellationToken token = default);
}
