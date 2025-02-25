// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Districts.Dtos;

namespace server.src.Application.Geography.Administrative.Districts.Interfaces;

/// <summary>
/// Defines methods for managing geography districts, including creation, updating, and deletion.
/// </summary>
public interface IDistrictCommands
{
    /// <summary>
    /// Initializes multiple geography districts in bulk.
    /// </summary>
    /// <param name="dto">A list of district data transfer objects to be created.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the success or failure of the bulk initialization process.</returns>
    Task<Response<string>> InitializeDistrictsAsync(List<CreateDistrictDto> dto, 
        CancellationToken token = default);

    /// <summary>
    /// Creates a new geography district.
    /// </summary>
    /// <param name="dto">The data transfer object containing district details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the operation.</returns>
    Task<Response<string>> CreateDistrictAsync(CreateDistrictDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Updates an existing geography district.
    /// </summary>
    /// <param name="id">The unique identifier of the district to update.</param>
    /// <param name="dto">The data transfer object containing updated district details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the update operation.</returns>
    Task<Response<string>> UpdateDistrictAsync(Guid id, UpdateDistrictDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Activates a district by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the district to activate.</param>
    /// <param name="version">The version of the district for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the activation was successful.</returns>
    Task<Response<string>> ActivateDistrictAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deactivates a district by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the district to deactivate.</param>
    /// <param name="version">The version of the district for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the deactivation was successful.</returns>
    Task<Response<string>> DeactivateDistrictAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deletes a geography district by its unique identifier and version.
    /// </summary>
    /// <param name="id">The unique identifier of the geography district to delete.</param>
    /// <param name="version">The version of the geography district to ensure concurrency control during deletion.</param>
    /// <param name="token">An optional cancellation token to cancel the operation if needed.</param>
    /// <returns>A response containing the result message of the deletion operation, including success or failure details.</returns>
    Task<Response<string>> DeleteDistrictAsync(Guid id, 
        Guid version, CancellationToken token = default);
}
