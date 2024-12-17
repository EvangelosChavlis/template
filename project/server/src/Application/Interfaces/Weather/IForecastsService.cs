// source
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Domain.Models.Common;

namespace server.src.Application.Interfaces.Weather;

/// <summary>
/// Interface for managing weather forecasts, including retrieval, creation, updating, and deletion of forecasts.
/// </summary>
public interface IForecastsService
{
    /// <summary>
    /// Retrieves a paginated list of weather forecasts based on the specified query parameters.
    /// </summary>
    /// <param name="pageParams">Query parameters for pagination and filtering.</param>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a ListResponse of forecast items.</returns>
    Task<ListResponse<List<ListItemForecastDto>>> GetForecastsService(UrlQuery pageParams, CancellationToken token = default);

    /// <summary>
    /// Retrieves detailed weather forecast data for a specific forecast identified by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the forecast.</param>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an ItemResponse with forecast details.</returns>
    Task<ItemResponse<ItemForecastDto>> GetForecastByIdService(Guid id, CancellationToken token = default);

    /// <summary>
    /// Initializes a list of forecasts in the system.
    /// </summary>
    /// <param name="dtos">The list of forecast data transfer objects to be initialized.</param>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse indicating the operation's outcome.</returns>
    Task<CommandResponse<string>> InitializeForecastsService(List<ForecastDto> dtos, CancellationToken token = default);

    /// <summary>
    /// Creates a new forecast in the system.
    /// </summary>
    /// <param name="dto">The data transfer object containing the forecast details.</param>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse indicating the operation's outcome.</returns>
    Task<CommandResponse<string>> CreateForecastService(ForecastDto dto, CancellationToken token = default);

    /// <summary>
    /// Updates an existing forecast identified by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the forecast to be updated.</param>
    /// <param name="dto">The data transfer object containing the updated forecast details.</param>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse indicating the operation's outcome.</returns>
    Task<CommandResponse<string>> UpdateForecastService(Guid id, ForecastDto dto, CancellationToken token = default);

    /// <summary>
    /// Deletes an existing forecast identified by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the forecast to be deleted.</param>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a CommandResponse indicating the operation's outcome.</returns>
    Task<CommandResponse<string>> DeleteForecastService(Guid id, CancellationToken token = default);
}
