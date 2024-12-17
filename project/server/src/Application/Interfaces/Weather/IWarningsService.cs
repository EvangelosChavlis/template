// source
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Domain.Models.Common;

namespace server.src.Application.Interfaces.Weather;

/// <summary>
/// Interface for managing weather warnings, including retrieval, creation, updating, and deletion of warnings.
/// </summary>
public interface IWarningsService
{
    /// <summary>
    /// Retrieves a paginated list of weather warnings based on the specified query parameters.
    /// </summary>
    /// <param name="pageParams">Query parameters for pagination and filtering.</param>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a ListResponse of warning items.</returns>
    Task<ListResponse<List<ListItemWarningDto>>> GetWarningsService(UrlQuery pageParams, CancellationToken token = default);

    /// <summary>
    /// Retrieves a list of warning picker items for selection purposes.
    /// </summary>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an ItemResponse with a list of picker warning items.</returns>
    Task<ItemResponse<List<PickerWarningDto>>> GetWarningsPickerService(CancellationToken token = default);

    /// <summary>
    /// Retrieves detailed warning data for a specific warning identified by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the warning.</param>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an ItemResponse with warning details.</returns>
    Task<ItemResponse<ItemWarningDto>> GetWarningByIdService(Guid id, CancellationToken token = default);

    /// <summary>
    /// Initializes a list of warnings in the system.
    /// </summary>
    /// <param name="dtos">The list of warning data transfer objects to be initialized.</param>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse indicating the operation's outcome.</returns>
    Task<CommandResponse<string>> InitializeWarningsService(List<WarningDto> dtos, CancellationToken token = default);

    /// <summary>
    /// Creates a new warning in the system.
    /// </summary>
    /// <param name="dto">The data transfer object containing the warning details.</param>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse indicating the operation's outcome.</returns>
    Task<CommandResponse<string>> CreateWarningService(WarningDto dto, CancellationToken token = default);

    /// <summary>
    /// Updates an existing warning identified by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the warning to be updated.</param>
    /// <param name="dto">The data transfer object containing the updated warning details.</param>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse indicating the operation's outcome.</returns>
    Task<CommandResponse<string>> UpdateWarningService(Guid id, WarningDto dto, CancellationToken token = default);

    /// <summary>
    /// Deletes an existing warning identified by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the warning to be deleted.</param>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse indicating the operation's outcome.</returns>
    Task<CommandResponse<string>> DeleteWarningService(Guid id, CancellationToken token = default);
}
