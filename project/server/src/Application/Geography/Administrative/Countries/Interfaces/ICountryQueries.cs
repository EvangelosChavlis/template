// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Countries.Dtos;

namespace server.src.Application.Geography.Administrative.Countries.Interfaces;

/// <summary>
/// Defines methods for retrieving geography countries.
/// </summary>
public interface ICountryQueries
{
    /// <summary>
    /// Retrieves a paginated list of geography countries based on query parameters.
    /// </summary>
    /// <param name="urlQuery">Query parameters for filtering, sorting, and pagination.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A paginated list of geography countrys.</returns>
    Task<ListResponse<List<ListItemCountryDto>>> GetCountriesAsync(UrlQuery urlQuery, 
        CancellationToken token = default);

    /// <summary>
    /// Retrieves detailed information about a specific geography terrain type by its unique ID.
    /// </summary>
    /// <param name="id">The unique identifier of the geography terrain type.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>Detailed information about the specified geography terrain type.</returns>
    Task<Response<ItemCountryDto>> GetCountryByIdAsync(Guid id, 
        CancellationToken token = default);
}
