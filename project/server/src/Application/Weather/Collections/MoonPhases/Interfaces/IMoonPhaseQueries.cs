// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Weather.Collections.MoonPhases.Dtos;

namespace server.src.Application.Weather.Collections.MoonPhases.Interfaces;

/// <summary>
/// Defines methods for retrieving weather moon phases.
/// </summary>
public interface IMoonPhaseQueries
{
    /// <summary>
    /// Retrieves a paginated list of weather moon phases based on query parameters.
    /// </summary>
    /// <param name="urlQuery">Query parameters for filtering, sorting, and pagination.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A paginated list of weather moonphases.</returns>
    Task<ListResponse<List<ListItemMoonPhaseDto>>> GetMoonPhasesAsync(UrlQuery urlQuery, 
        CancellationToken token = default);

    /// <summary>
    /// Retrieves a list of weather moon phases formatted for selection purposes.
    /// </summary>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A list of weather moonphases for selection.</returns>
    Task<Response<List<PickerMoonPhaseDto>>> GetMoonPhasesPickerAsync(CancellationToken token = default);

    /// <summary>
    /// Retrieves detailed information about a specific weather moon phase by its unique ID.
    /// </summary>
    /// <param name="id">The unique identifier of the weather moon phase.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>Detailed information about the specified weather moon phase.</returns>
    Task<Response<ItemMoonPhaseDto>> GetMoonPhaseByIdAsync(Guid id, 
        CancellationToken token = default);
}
