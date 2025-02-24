// source
using server.src.Domain.Geography.Natural.Locations.Dtos;
using server.src.Domain.Geography.Natural.Locations.Models;

namespace server.src.Application.Geography.Natural.Locations.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="Location"/> model  
/// into an <see cref="ItemLocationDto"/> for detailed item representation.
/// </summary>
public static class ItemLocationDtoMapper
{
    /// <summary>
    /// Maps a <see cref="Location"/> model to an <see cref="ItemLocationDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="Location"/> model that will be mapped to an <see cref="ItemLocationDto"/>.</param>
    /// <returns>An <see cref="ItemLocationDto"/> representing the <see cref="Location"/> model with full details.</returns>
    public static ItemLocationDto ItemLocationDtoMapping(this Location model) => 
        new(
            Longitude: model.Longitude,
            Latitude: model.Latitude,
            Altitude: model.Altitude,
            IsActive: model.IsActive,
            Timezone: model.Timezone.Name,
            TerrainType: model.TerrainType.Name,
            ClimateZone: model.ClimateZone.Name,
            Version: model.Version
        );
}