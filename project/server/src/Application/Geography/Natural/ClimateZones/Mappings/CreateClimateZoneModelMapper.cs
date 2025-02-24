// source
using server.src.Domain.Geography.Natural.ClimateZones.Dtos;
using server.src.Domain.Geography.Natural.ClimateZones.Models;

namespace server.src.Application.Geography.Natural.ClimateZones.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="CreateClimateZoneDto"/> into a <see cref="ClimateZone"/> model.
/// This utility class is used to create new terrain type instances based on provided data transfer objects.
/// </summary>
public static class CreateClimateZoneModelMapper
{
    /// <summary>
    /// Maps a <see cref="CreateClimateZoneDto"/> to a <see cref="ClimateZone"/> model, creating a new climatezone entity.
    /// </summary>
    /// <param name="dto">The data transfer object containing climatezone details.</param>
    /// <returns>A newly created <see cref="ClimateZone"/> model populated with data from the provided DTO.</returns>
    public static ClimateZone CreateClimateZoneModelMapping(this CreateClimateZoneDto dto)
        => new()
        {
            Name = dto.Name,
            Description = dto.Description,
            AvgTemperatureC = dto.AvgTemperatureC,
            AvgPrecipitationMm = dto.AvgPrecipitationMm,
            IsActive = true,
            Version = Guid.NewGuid()
        };
}
