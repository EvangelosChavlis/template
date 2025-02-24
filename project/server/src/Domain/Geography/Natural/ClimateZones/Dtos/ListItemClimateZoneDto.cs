namespace server.src.Domain.Geography.Natural.ClimateZones.Dtos;

/// <summary>
/// Represents a lightweight data transfer object (DTO) for listing climate zones.  
/// Contains key details such as ID, name, average temperature, precipitation, and active status.  
/// Used for displaying climate zones in summary views or lists.
/// </summary>
public record ListItemClimateZoneDto(
    Guid Id,
    string Name,
    double AvgTemperatureC,
    double AvgPrecipitationMm,
    bool IsActive
);