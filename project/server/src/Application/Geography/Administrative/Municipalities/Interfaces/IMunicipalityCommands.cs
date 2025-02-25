// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Municipalities.Dtos;

namespace server.src.Application.Geography.Administrative.Municipalities.Interfaces;

/// <summary>
/// Defines methods for managing geography municipalities, including creation, updating, and deletion.
/// </summary>
public interface IMunicipalityCommands
{
    /// <summary>
    /// Initializes multiple geography municipalities in bulk.
    /// </summary>
    /// <param name="dto">A list of municipality data transfer objects to be created.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the success or failure of the bulk initialization process.</returns>
    Task<Response<string>> InitializeMunicipalitiesAsync(List<CreateMunicipalityDto> dto, 
        CancellationToken token = default);

    /// <summary>
    /// Creates a new geography municipality.
    /// </summary>
    /// <param name="dto">The data transfer object containing municipality details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the operation.</returns>
    Task<Response<string>> CreateMunicipalityAsync(CreateMunicipalityDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Updates an existing geography municipality.
    /// </summary>
    /// <param name="id">The unique identifier of the municipality to update.</param>
    /// <param name="dto">The data transfer object containing updated municipality details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the update operation.</returns>
    Task<Response<string>> UpdateMunicipalityAsync(Guid id, UpdateMunicipalityDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Activates a municipality by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the municipality to activate.</param>
    /// <param name="version">The version of the municipality for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the activation was successful.</returns>
    Task<Response<string>> ActivateMunicipalityAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deactivates a municipality by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the municipality to deactivate.</param>
    /// <param name="version">The version of the municipality for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the deactivation was successful.</returns>
    Task<Response<string>> DeactivateMunicipalityAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deletes a geography municipality by its unique identifier and version.
    /// </summary>
    /// <param name="id">The unique identifier of the geography municipality to delete.</param>
    /// <param name="version">The version of the geography municipality to ensure concurrency control during deletion.</param>
    /// <param name="token">An optional cancellation token to cancel the operation if needed.</param>
    /// <returns>A response containing the result message of the deletion operation, including success or failure details.</returns>
    Task<Response<string>> DeleteMunicipalityAsync(Guid id, 
        Guid version, CancellationToken token = default);
}
