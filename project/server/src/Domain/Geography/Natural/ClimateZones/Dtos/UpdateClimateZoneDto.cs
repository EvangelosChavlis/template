namespace server.src.Domain.Geography.Natural.ClimateZones.Dtos;

/// <summary>
/// Represents the data transfer object (DTO) for updating an existing climate zone.  
/// Includes fields for modifying the name, description, average climate data, and versioning.  
/// Ensures proper version control to manage concurrent updates.
/// </summary>
public record UpdateClimateZoneDto(
    string Name,
    string Description,
    string Code,
    double AvgTemperatureC,
    double AvgPrecipitationMm,
    Guid Version
);