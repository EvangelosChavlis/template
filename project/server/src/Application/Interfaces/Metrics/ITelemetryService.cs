// source
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Metrics;
using server.src.Domain.Models.Common;

namespace server.src.Application.Interfaces.Metrics;

/// <summary>
/// Interface for managing telemetry metrics, including retrieval of telemetry data and details.
/// </summary>
public interface ITelemetryService
{
    /// <summary>
    /// Retrieves a paginated list of telemetry data based on the specified query parameters.
    /// </summary>
    /// <param name="pageParams">Query parameters for pagination and filtering.</param>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a ListResponse of telemetry items.</returns>
    Task<ListResponse<List<ListItemTelemetryDto>>> GetTelemetryService(UrlQuery pageParams, CancellationToken token = default); 

    /// <summary>
    /// Retrieves detailed telemetry data for a specific record identified by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the telemetry record.</param>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an ItemResponse with telemetry details.</returns>
    Task<ItemResponse<ItemTelemetryDto>> GetTelemetryByIdService(Guid id, CancellationToken token = default);
}
