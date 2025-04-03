namespace server.src.Domain.Geography.Natural.Locations.Dtos;

/// <summary>
/// Represents the data transfer object (DTO) for creating a new location.  
/// Includes geographic coordinates (longitude, latitude, altitude) and references  
/// to associated entities such as timezone, surface type, and climate zone.
/// </summary>
/// <param name="Longitude">The longitude coordinate of the location.</param>
/// <param name="Latitude">The latitude coordinate of the location.</param>
/// <param name="Altitude">The altitude of the location in meters above sea level.</param>
/// <param name="Depth">The depth of the location in meters below sea level.</param>
/// <param name="TimezoneId">The unique identifier of the associated timezone.</param>
/// <param name="NaturalFeatureId">The unique identifier of the associated natural feature.</param>
/// <param name="SurfaceTypeId">The unique identifier of the associated surface type.</param>
/// <param name="ClimateZoneId">The unique identifier of the associated climate zone.</param>
public record CreateLocationDto(
    double Longitude,
    double Latitude,
    double Altitude,
    double Depth,
    Guid TimezoneId,
    Guid NaturalFeatureId,
    Guid SurfaceTypeId,
    Guid ClimateZoneId
);