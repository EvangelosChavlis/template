// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.Timezones.Dtos;

namespace server.src.Application.Geography.Natural.Timezones.Interfaces;

/// <summary>
/// Defines methods for retrieving geography timezones.
/// </summary>
public interface ITimezoneQueries
{
    /// <summary>
    /// Retrieves a paginated list of geography timezones based on query parameters.
    /// </summary>
    /// <param name="urlQuery">Query parameters for filtering, sorting, and pagination.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A paginated list of geography timezones.</returns>
    Task<ListResponse<List<ListItemTimezoneDto>>> GetTimezonesAsync(UrlQuery urlQuery, 
        CancellationToken token = default);

    /// <summary>
    /// Retrieves a list of geography timezones formatted for selection purposes.
    /// </summary>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A list of geography timezones for selection.</returns>
    Task<Response<List<PickerTimezoneDto>>> GetTimezonesPickerAsync(CancellationToken token = default);

    /// <summary>
    /// Retrieves detailed information about a specific geography surface type by its unique ID.
    /// </summary>
    /// <param name="id">The unique identifier of the geography surface type.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>Detailed information about the specified geography surface type.</returns>
    Task<Response<ItemTimezoneDto>> GetTimezoneByIdAsync(Guid id, 
        CancellationToken token = default);
}
