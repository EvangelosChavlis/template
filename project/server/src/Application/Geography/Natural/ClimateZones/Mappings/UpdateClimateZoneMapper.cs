// source
using server.src.Domain.Geography.Natural.ClimateZones.Dtos;
using server.src.Domain.Geography.Natural.ClimateZones.Models;

namespace server.src.Application.Geography.Natural.ClimateZones.Mappings;

/// <summary>
/// Provides mapping functionality to update an existing <see cref="ClimateZone"/> model 
/// using data from an <see cref="UpdateClimateZoneDto"/>.
/// This utility class ensures that the climate zone entity is updated efficiently with new details.
/// </summary>
public static class UpdateClimateZoneMapper
{
    /// <summary>
    /// Updates an existing <see cref="ClimateZone"/> model with data from an <see cref="UpdateClimateZoneDto"/>.
    /// </summary>
    /// <param name="dto">The data transfer object containing updated climate zone details.</param>
    /// <param name="model">The existing <see cref="ClimateZone"/> model to be updated.</param>
    public static void UpdateClimateZoneMapping(this UpdateClimateZoneDto dto, ClimateZone model)
    {
        model.Name = dto.Name;
        model.Description = dto.Description;
        model.Code = dto.Code;
        model.AvgTemperatureC = dto.AvgTemperatureC;
        model.AvgPrecipitationMm = dto.AvgPrecipitationMm;
        model.IsActive = model.IsActive;
        model.Version = Guid.NewGuid();
    }
}
