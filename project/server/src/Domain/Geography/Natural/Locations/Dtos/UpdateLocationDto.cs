namespace server.src.Domain.Geography.Natural.Locations.Dtos;

/// <summary>
/// Represents the data transfer object (DTO) for updating an existing location.  
/// Includes geographic coordinates, associated timezone, terrain type, climate zone, and versioning for concurrency control.
/// </summary>
/// <param name="Longitude">The longitude coordinate of the location.</param>
/// <param name="Latitude">The latitude coordinate of the location.</param>
/// <param name="Altitude">The altitude of the location in meters above sea level.</param>
/// <param name="TimezoneId">The unique identifier of the associated timezone.</param>
/// <param name="TerrainTypeId">The unique identifier of the associated terrain type.</param>
/// <param name="ClimateZoneId">The unique identifier of the associated climate zone.</param>
/// <param name="Version">The version identifier to ensure proper concurrency control during updates.</param>
public record UpdateLocationDto(
    double Longitude,
    double Latitude,
    double Altitude,
    Guid TimezoneId,
    Guid TerrainTypeId,
    Guid ClimateZoneId,
    Guid Version
);