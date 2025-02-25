// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Municipalities.Dtos;

namespace server.src.Application.Geography.Administrative.Municipalities.Interfaces;

/// <summary>
/// Defines methods for retrieving geography municipalities.
/// </summary>
public interface IMunicipalityQueries
{
    /// <summary>
    /// Retrieves a paginated list of geography municipalities based on query parameters.
    /// </summary>
    /// <param name="urlQuery">Query parameters for filtering, sorting, and pagination.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A paginated list of geography municipalities.</returns>
    Task<ListResponse<List<ListItemMunicipalityDto>>> GetMunicipalitiesAsync(UrlQuery urlQuery, 
        CancellationToken token = default);

    /// <summary>
    /// Retrieves detailed information about a specific geography municipality by its unique ID.
    /// </summary>
    /// <param name="id">The unique identifier of the geography municipality.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>Detailed information about the specified geography municipality.</returns>
    Task<Response<ItemMunicipalityDto>> GetMunicipalityByIdAsync(Guid id, 
        CancellationToken token = default);
}
