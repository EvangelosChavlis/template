// source
using server.src.Domain.Geography.Natural.Locations.Dtos;

namespace server.src.Application.Geography.Natural.Locations.Mappings;

/// <summary>
/// Provides a method to generate an <see cref="ItemLocationDto"/> with default values  
/// to represent an error state when a valid location cannot be retrieved.
/// </summary>
public static class ErrorItemLocationDtoMapper
{
    /// <summary>
    /// Creates an <see cref="ItemLocationDto"/> with default values in case of an error.
    /// </summary>
    /// <returns>An <see cref="ItemLocationDto"/> with empty or placeholder values, indicating an error state.</returns>
    public static ItemLocationDto ErrorItemLocationDtoMapping() => 
        new(
            Longitude: double.MinValue,
            Latitude: double.MinValue,
            Altitude: double.MinValue,
            Depth: double.MinValue,
            IsActive: false,
            Timezone: string.Empty,
            NaturalFeature: string.Empty,
            SurfaceType: string.Empty,
            ClimateZone: string.Empty,
            Version: Guid.Empty
        );
}