namespace server.src.Domain.Geography.Natural.Locations.Dtos;

/// <summary>
/// Represents the data transfer object (DTO) for creating a new location.  
/// Includes geographic coordinates (longitude, latitude, altitude) and references  
/// to associated entities such as timezone, terrain type, and climate zone.
/// </summary>
/// <param name="Longitude">The longitude coordinate of the location.</param>
/// <param name="Latitude">The latitude coordinate of the location.</param>
/// <param name="Altitude">The altitude of the location in meters above sea level.</param>
/// <param name="TimezoneId">The unique identifier of the associated timezone.</param>
/// <param name="TerrainTypeId">The unique identifier of the associated terrain type.</param>
/// <param name="ClimateZoneId">The unique identifier of the associated climate zone.</param>
public record CreateLocationDto(
    double Longitude,
    double Latitude,
    double Altitude,
    Guid TimezoneId,
    Guid TerrainTypeId,
    Guid ClimateZoneId
);