// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.Locations.Dtos;

namespace server.src.Application.Geography.Natural.Locations.Interfaces;

/// <summary>
/// Defines methods for retrieving geography locations.
/// </summary>
public interface ILocationQueries
{
    /// <summary>
    /// Retrieves a paginated list of geography locations based on query parameters.
    /// </summary>
    /// <param name="urlQuery">Query parameters for filtering, sorting, and pagination.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A paginated list of geography locations.</returns>
    Task<ListResponse<List<ListItemLocationDto>>> GetLocationsAsync(UrlQuery urlQuery, 
        CancellationToken token = default);

    /// <summary>
    /// Retrieves detailed information about a specific geography terrain type by its unique ID.
    /// </summary>
    /// <param name="id">The unique identifier of the geography terrain type.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>Detailed information about the specified geography terrain type.</returns>
    Task<Response<ItemLocationDto>> GetLocationByIdAsync(Guid id, 
        CancellationToken token = default);
}
