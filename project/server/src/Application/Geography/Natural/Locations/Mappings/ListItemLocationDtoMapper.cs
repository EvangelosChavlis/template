using server.src.Domain.Geography.Natural.Locations.Dtos;
using server.src.Domain.Geography.Natural.Locations.Models;

namespace server.src.Application.Geography.Natural.Locations.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="Location"/> model  
/// into a <see cref="ListItemLocationDto"/> for list-based views with key details.
/// </summary>
public static class ListItemLocationDtoMapper
{
    /// <summary>
    /// Maps a <see cref="Location"/> model to a <see cref="ListItemLocationDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="Location"/> model that will be mapped.</param>
    /// <returns>A <see cref="ListItemLocationDto"/> representing the location with essential details.</returns>
    public static ListItemLocationDto ListItemLocationDtoMapping(
        this Location model) => new(
            Longitude: model.Longitude,
            Latitude: model.Latitude,
            Altitude: model.Altitude,
            IsActive: model.IsActive
        );
}