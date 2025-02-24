// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Continents.Dtos;

namespace server.src.Application.Geography.Administrative.Continents.Interfaces;

/// <summary>
/// Defines methods for retrieving geography continents.
/// </summary>
public interface IContinentQueries
{
    /// <summary>
    /// Retrieves a paginated list of geography continents based on query parameters.
    /// </summary>
    /// <param name="urlQuery">Query parameters for filtering, sorting, and pagination.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A paginated list of geography continents.</returns>
    Task<ListResponse<List<ListItemContinentDto>>> GetContinentsAsync(UrlQuery urlQuery, 
        CancellationToken token = default);

    /// <summary>
    /// Retrieves detailed information about a specific geography terrain type by its unique ID.
    /// </summary>
    /// <param name="id">The unique identifier of the geography terrain type.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>Detailed information about the specified geography terrain type.</returns>
    Task<Response<ItemContinentDto>> GetContinentByIdAsync(Guid id, 
        CancellationToken token = default);
}
