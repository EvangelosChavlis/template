// source
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Domain.Models.Common;

namespace server.src.Application.Weather.Warnings.Interfaces;

/// <summary>
/// Defines methods for retrieving weather warnings.
/// </summary>
public interface IWarningQueries
{
    /// <summary>
    /// Retrieves a paginated list of weather warnings based on query parameters.
    /// </summary>
    /// <param name="urlQuery">Query parameters for filtering, sorting, and pagination.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A paginated list of weather warnings.</returns>
    Task<ListResponse<List<ListItemWarningDto>>> GetWarningsAsync(UrlQuery urlQuery, 
        CancellationToken token = default);

    /// <summary>
    /// Retrieves a list of weather warnings formatted for selection purposes.
    /// </summary>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A list of weather warnings for selection.</returns>
    Task<Response<List<PickerWarningDto>>> GetWarningsPickerAsync(CancellationToken token = default);

    /// <summary>
    /// Retrieves detailed information about a specific weather warning by its unique ID.
    /// </summary>
    /// <param name="id">The unique identifier of the weather warning.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>Detailed information about the specified weather warning.</returns>
    Task<Response<ItemWarningDto>> GetWarningByIdAsync(Guid id, 
        CancellationToken token = default);
}
