namespace server.src.Domain.Geography.Natural.ClimateZones.Dtos;

/// <summary>
/// Represents a detailed data transfer object (DTO) for a climate zone.  
/// Includes full descriptive information, average climate data, status, and versioning.  
/// Used for individual item views where complete details are required.
/// </summary>
public record ItemClimateZoneDto(
    Guid Id,
    string Name,
    string Description,
    double AvgTemperatureC,
    double AvgPrecipitationMm,
    bool IsActive,
    Guid Version
);