// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.SurfaceTypes.Dtos;

namespace server.src.Application.Geography.Natural.SurfaceTypes.Interfaces;

/// <summary>
/// Defines methods for retrieving geography surface type.
/// </summary>
public interface ISurfaceTypeQueries
{
    /// <summary>
    /// Retrieves a paginated list of geography surface type based on query parameters.
    /// </summary>
    /// <param name="urlQuery">Query parameters for filtering, sorting, and pagination.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A paginated list of geography SurfaceTypes.</returns>
    Task<ListResponse<List<ListItemSurfaceTypeDto>>> GetSurfaceTypesAsync(UrlQuery urlQuery, 
        CancellationToken token = default);

    /// <summary>
    /// Retrieves a list of geography surface type formatted for selection purposes.
    /// </summary>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A list of geography SurfaceTypes for selection.</returns>
    Task<Response<List<PickerSurfaceTypeDto>>> GetSurfaceTypesPickerAsync(CancellationToken token = default);

    /// <summary>
    /// Retrieves detailed information about a specific geography surface type by its unique ID.
    /// </summary>
    /// <param name="id">The unique identifier of the geography surface type.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>Detailed information about the specified geography surface type.</returns>
    Task<Response<ItemSurfaceTypeDto>> GetSurfaceTypeByIdAsync(Guid id, 
        CancellationToken token = default);
}
