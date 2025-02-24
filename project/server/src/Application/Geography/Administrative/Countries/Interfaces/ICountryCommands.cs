// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Countries.Dtos;

namespace server.src.Application.Geography.Administrative.Countries.Interfaces;

/// <summary>
/// Defines methods for managing geography countrys, including creation, updating, and deletion.
/// </summary>
public interface ICountryCommands
{
    /// <summary>
    /// Initializes multiple geography countries in bulk.
    /// </summary>
    /// <param name="dto">A list of country data transfer objects to be created.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response indicating the success or failure of the bulk initialization process.</returns>
    Task<Response<string>> InitializeCountriesAsync(List<CreateCountryDto> dto, 
        CancellationToken token = default);

    /// <summary>
    /// Creates a new geography country.
    /// </summary>
    /// <param name="dto">The data transfer object containing country details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the operation.</returns>
    Task<Response<string>> CreateCountryAsync(CreateCountryDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Updates an existing geography country.
    /// </summary>
    /// <param name="id">The unique identifier of the country to update.</param>
    /// <param name="dto">The data transfer object containing updated country details.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A response containing the result message of the update operation.</returns>
    Task<Response<string>> UpdateCountryAsync(Guid id, UpdateCountryDto dto, 
        CancellationToken token = default);

    /// <summary>
    /// Activates a country by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the country to activate.</param>
    /// <param name="version">The version of the country for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the activation was successful.</returns>
    Task<Response<string>> ActivateCountryAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deactivates a country by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the country to deactivate.</param>
    /// <param name="version">The version of the country for concurrency control.</param>
    /// <param name="token">Optional cancellation token to cancel the operation.</param>
    /// <returns>A response indicating whether the deactivation was successful.</returns>
    Task<Response<string>> DeactivateCountryAsync(Guid id, 
        Guid version, CancellationToken token = default);

    /// <summary>
    /// Deletes a geography country by its unique identifier and version.
    /// </summary>
    /// <param name="id">The unique identifier of the geography country to delete.</param>
    /// <param name="version">The version of the geography country to ensure concurrency control during deletion.</param>
    /// <param name="token">An optional cancellation token to cancel the operation if needed.</param>
    /// <returns>A response containing the result message of the deletion operation, including success or failure details.</returns>
    Task<Response<string>> DeleteCountryAsync(Guid id, 
        Guid version, CancellationToken token = default);
}
