namespace server.src.Domain.Geography.Natural.Locations.Dtos;

/// <summary>
/// Represents a simplified data transfer object (DTO) for listing location details.  
/// Includes essential geographic coordinates and status information.  
/// Used in scenarios where only basic location data is required.
/// </summary>
public record ListItemLocationDto(
    double Longitude,
    double Latitude,
    double Altitude,
    bool IsActive
);