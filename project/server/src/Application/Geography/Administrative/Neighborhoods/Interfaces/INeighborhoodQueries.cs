// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;

namespace server.src.Application.Geography.Administrative.Neighborhoods.Interfaces;

/// <summary>
/// Defines methods for retrieving geography neighborhoods.
/// </summary>
public interface INeighborhoodQueries
{
    /// <summary>
    /// Retrieves a paginated list of geography neighborhoods based on query parameters.
    /// </summary>
    /// <param name="urlQuery">Query parameters for filtering, sorting, and pagination.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A paginated list of geography neighborhoods.</returns>
    Task<ListResponse<List<ListItemNeighborhoodDto>>> GetNeighborhoodsAsync(UrlQuery urlQuery, 
        CancellationToken token = default);

    /// <summary>
    /// Retrieves detailed information about a specific geography neighborhood by its unique ID.
    /// </summary>
    /// <param name="id">The unique identifier of the geography neighborhood.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>Detailed information about the specified geography neighborhood.</returns>
    Task<Response<ItemNeighborhoodDto>> GetNeighborhoodByIdAsync(Guid id, 
        CancellationToken token = default);
}
