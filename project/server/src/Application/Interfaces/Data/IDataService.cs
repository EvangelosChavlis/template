// source
using server.src.Domain.Dto.Common;

namespace server.src.Application.Interfaces.Data;

/// <summary>
/// Interface for managing data-related operations, such as seeding and clearing data.
/// </summary>
public interface IDataService
{
    /// <summary>
    /// Seeds initial data into the system's database or storage.
    /// </summary>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse with a status message.</returns>
    Task<CommandResponse<string>> SeedDataAsync(CancellationToken token = default);

    /// <summary>
    /// Clears all data from the system's database or storage.
    /// </summary>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse with a status message.</returns>
    Task<CommandResponse<string>> ClearDataAsync(CancellationToken token = default);
}
