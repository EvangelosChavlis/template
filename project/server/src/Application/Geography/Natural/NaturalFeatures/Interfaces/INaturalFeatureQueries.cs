// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.NaturalFeatures.Dtos;

namespace server.src.Application.Geography.Natural.NaturalFeatures.Interfaces;

/// <summary>
/// Defines methods for retrieving geography natural feature.
/// </summary>
public interface INaturalFeatureQueries
{
    /// <summary>
    /// Retrieves a paginated list of geography natural feature based on query parameters.
    /// </summary>
    /// <param name="urlQuery">Query parameters for filtering, sorting, and pagination.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A paginated list of geography NaturalFeatures.</returns>
    Task<ListResponse<List<ListItemNaturalFeatureDto>>> GetNaturalFeaturesAsync(UrlQuery urlQuery, 
        CancellationToken token = default);

    /// <summary>
    /// Retrieves a list of geography natural feature formatted for selection purposes.
    /// </summary>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A list of geography NaturalFeatures for selection.</returns>
    Task<Response<List<PickerNaturalFeatureDto>>> GetNaturalFeaturesPickerAsync(CancellationToken token = default);

    /// <summary>
    /// Retrieves detailed information about a specific geography natural feature by its unique ID.
    /// </summary>
    /// <param name="id">The unique identifier of the geography natural feature.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>Detailed information about the specified geography natural feature.</returns>
    Task<Response<ItemNaturalFeatureDto>> GetNaturalFeatureByIdAsync(Guid id, 
        CancellationToken token = default);
}
