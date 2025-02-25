// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Regions.Dtos;

namespace server.src.Application.Geography.Administrative.Regions.Interfaces;

/// <summary>
/// Defines methods for retrieving geography regions.
/// </summary>
public interface IRegionQueries
{
    /// <summary>
    /// Retrieves a paginated list of geography regions based on query parameters.
    /// </summary>
    /// <param name="urlQuery">Query parameters for filtering, sorting, and pagination.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A paginated list of geography regions.</returns>
    Task<ListResponse<List<ListItemRegionDto>>> GetRegionsAsync(UrlQuery urlQuery, 
        CancellationToken token = default);

    /// <summary>
    /// Retrieves detailed information about a specific geography region by its unique ID.
    /// </summary>
    /// <param name="id">The unique identifier of the geography region.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>Detailed information about the specified geography region.</returns>
    Task<Response<ItemRegionDto>> GetRegionByIdAsync(Guid id, 
        CancellationToken token = default);
}
