// source
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.ClimateZones.Dtos;

namespace server.src.Application.Geography.Natural.ClimateZones.Interfaces;

/// <summary>
/// Defines methods for retrieving geography climate zone.
/// </summary>
public interface IClimateZoneQueries
{
    /// <summary>
    /// Retrieves a paginated list of geography climate zone based on query parameters.
    /// </summary>
    /// <param name="urlQuery">Query parameters for filtering, sorting, and pagination.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A paginated list of geography climate zones.</returns>
    Task<ListResponse<List<ListItemClimateZoneDto>>> GetClimateZonesAsync(UrlQuery urlQuery, 
        CancellationToken token = default);

    /// <summary>
    /// Retrieves a list of geography climate zone formatted for selection purposes.
    /// </summary>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>A list of geography climate zones for selection.</returns>
    Task<Response<List<PickerClimateZoneDto>>> GetClimateZonesPickerAsync(CancellationToken token = default);

    /// <summary>
    /// Retrieves detailed information about a specific geography climate zone by its unique ID.
    /// </summary>
    /// <param name="id">The unique identifier of the geography climate zone.</param>
    /// <param name="token">Optional cancellation token.</param>
    /// <returns>Detailed information about the specified geography climate zone.</returns>
    Task<Response<ItemClimateZoneDto>> GetClimateZoneByIdAsync(Guid id, 
        CancellationToken token = default);
}
