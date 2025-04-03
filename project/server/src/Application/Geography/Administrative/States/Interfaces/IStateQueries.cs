// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.States.Dtos;

namespace server.src.Application.Geography.Administrative.States.Interfaces;

/// <summary>
/// Defines methods for retrieving geography states.
/// </summary>
public interface IStateQueries
{
    /// <summary>
    /// Retrieves a paginated list of geography states based on query parameters.
    /// </summary>
    /// <param name="urlQuery">Query parameters for filtering, sorting, and pagination.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A paginated list of geography states.</returns>
    Task<ListResponse<List<ListItemStateDto>>> GetStatesAsync(UrlQuery urlQuery, 
        CancellationToken token = default);

    /// <summary>
    /// Retrieves detailed information about a specific geography surface type by its unique ID.
    /// </summary>
    /// <param name="id">The unique identifier of the geography surface type.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>Detailed information about the specified geography surface type.</returns>
    Task<Response<ItemStateDto>> GetStateByIdAsync(Guid id, 
        CancellationToken token = default);
}
