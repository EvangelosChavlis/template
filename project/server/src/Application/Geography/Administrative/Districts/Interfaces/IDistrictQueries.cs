// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Districts.Dtos;

namespace server.src.Application.Geography.Administrative.Districts.Interfaces;

/// <summary>
/// Defines methods for retrieving geography districts.
/// </summary>
public interface IDistrictQueries
{
    /// <summary>
    /// Retrieves a paginated list of geography districts based on query parameters.
    /// </summary>
    /// <param name="urlQuery">Query parameters for filtering, sorting, and pagination.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A paginated list of geography districts.</returns>
    Task<ListResponse<List<ListItemDistrictDto>>> GetDistrictsAsync(UrlQuery urlQuery, 
        CancellationToken token = default);

    /// <summary>
    /// Retrieves detailed information about a specific geography district by its unique ID.
    /// </summary>
    /// <param name="id">The unique identifier of the geography district.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>Detailed information about the specified geography district.</returns>
    Task<Response<ItemDistrictDto>> GetDistrictByIdAsync(Guid id, 
        CancellationToken token = default);
}
