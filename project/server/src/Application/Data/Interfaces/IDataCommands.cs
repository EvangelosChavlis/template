// source
using server.src.Domain.Common.Dtos;

namespace server.src.Application.Data.Interfaces;

/// <summary>
/// Interface for managing data-related operations, such as seeding and clearing data.
/// </summary>
public interface IDataCommands
{
    /// <summary>
    /// Seeds initial data into the system's database or storage.
    /// </summary>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse with a status message.</returns>
    Task<Response<string>> SeedDataAsync(CancellationToken token = default);

    Task<Response<string>> SeedContinentsAsync(string basePath, 
        CancellationToken token = default);

    Task<Response<string>> SeedCountriesAsync(string basePath,
        CancellationToken token = default);

    Task<Response<string>> SeedStatesAsync(string basePath, 
        CancellationToken token = default);

    Task<Response<string>> SeedRegionsAsync(string basePath, 
        CancellationToken token = default);

    Task<Response<string>> SeedMunicipalitiesAsync(string basePath,
        CancellationToken token = default);

    Task<Response<string>> SeedDistrictsAsync(string basePath,
        CancellationToken token = default);

    Task<Response<string>> SeedNeighborhoodAsync(string basePath,
        CancellationToken token = default);
    
    /// <summary>
    /// Clears all data from the system's database or storage.
    /// </summary>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse with a status message.</returns>
    Task<Response<string>> ClearDataAsync(CancellationToken token = default);
}
