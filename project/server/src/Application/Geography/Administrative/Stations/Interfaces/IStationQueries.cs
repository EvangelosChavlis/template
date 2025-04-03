// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Stations.Dtos;

namespace server.src.Application.Geography.Administrative.Stations.Interfaces;

/// <summary>
/// Defines methods for retrieving geography stations.
/// </summary>
public interface IStationQueries
{
    /// <summary>
    /// Retrieves a paginated list of geography stations based on query parameters.
    /// </summary>
    /// <param name="urlQuery">Query parameters for filtering, sorting, and pagination.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A paginated list of geography stations.</returns>
    Task<ListResponse<List<ListItemStationDto>>> GetStationsAsync(UrlQuery urlQuery, 
        CancellationToken token = default);

    /// <summary>
    /// Retrieves detailed information about a specific geography surface type by its unique ID.
    /// </summary>
    /// <param name="id">The unique identifier of the geography surface type.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>Detailed information about the specified geography surface type.</returns>
    Task<Response<ItemStationDto>> GetStationByIdAsync(Guid id, 
        CancellationToken token = default);
}
