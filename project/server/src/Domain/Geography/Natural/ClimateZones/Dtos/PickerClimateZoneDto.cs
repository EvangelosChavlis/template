namespace server.src.Domain.Geography.Natural.ClimateZones.Dtos;

/// <summary>
/// Represents a simplified climate zone data transfer object (DTO)  
/// used for selection or dropdown lists.
/// Contains only essential identifying information.
/// </summary>
public record PickerClimateZoneDto(
    Guid Id,
    string Name
);