// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.TerrainTypes.Dtos;

namespace server.src.Application.Geography.Natural.TerrainTypes.Interfaces;

/// <summary>
/// Defines methods for retrieving geography terrain type.
/// </summary>
public interface ITerrainTypeQueries
{
    /// <summary>
    /// Retrieves a paginated list of geography terrain type based on query parameters.
    /// </summary>
    /// <param name="urlQuery">Query parameters for filtering, sorting, and pagination.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A paginated list of geography terraintypes.</returns>
    Task<ListResponse<List<ListItemTerrainTypeDto>>> GetTerrainTypesAsync(UrlQuery urlQuery, 
        CancellationToken token = default);

    /// <summary>
    /// Retrieves a list of geography terrain type formatted for selection purposes.
    /// </summary>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A list of geography terraintypes for selection.</returns>
    Task<Response<List<PickerTerrainTypeDto>>> GetTerrainTypesPickerAsync(CancellationToken token = default);

    /// <summary>
    /// Retrieves detailed information about a specific geography terrain type by its unique ID.
    /// </summary>
    /// <param name="id">The unique identifier of the geography terrain type.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>Detailed information about the specified geography terrain type.</returns>
    Task<Response<ItemTerrainTypeDto>> GetTerrainTypeByIdAsync(Guid id, 
        CancellationToken token = default);
}
