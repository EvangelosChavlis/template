// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Metrics.LogErrors.Dtos;

namespace server.src.Application.Metrics.LogErrors.Interfaces;

/// <summary>
/// Interface for managing error-related metrics, including retrieval of error details and lists.
/// </summary>
public interface ILogErrorQueries
{
    /// <summary>
    /// Retrieves a paginated list of errors based on the specified query parameters.
    /// </summary>
    /// <param name="pageParams">Query parameters for pagination and filtering.</param>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a ListResponse of error items.</returns>
    Task<ListResponse<List<ListItemLogErrorDto>>> GetLogErrorsAsync(UrlQuery pageParams, CancellationToken token = default); 

    /// <summary>
    /// Retrieves detailed information about a specific error by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the error.</param>
    /// <param name="token">Cancellation token to allow the operation to be canceled.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an ItemResponse with error details.</returns>
    Task<Response<ItemLogErrorDto>> GetLogErrorByIdAsync(Guid id, CancellationToken token = default);
}
