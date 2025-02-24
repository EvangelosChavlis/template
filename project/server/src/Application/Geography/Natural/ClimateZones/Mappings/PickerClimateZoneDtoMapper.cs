// source
using server.src.Domain.Geography.Natural.ClimateZones.Dtos;
using server.src.Domain.Geography.Natural.ClimateZones.Models;

namespace server.src.Application.Geography.Natural.ClimateZones.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="ClimateZone"/> model 
/// into a <see cref="PickerClimateZoneDto"/>.
/// This mapper is used to transform climatezone data for selection lists or dropdowns.
/// </summary>
public static class PickerClimateZoneDtoMapper
{
    /// <summary>
    /// Maps a <see cref="ClimateZone"/> model to a <see cref="PickerClimateZoneDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="ClimateZone"/> model that will be mapped.</param>
    /// <returns>A <see cref="PickerClimateZoneDto"/> containing essential details for selection purposes.</returns>
    public static PickerClimateZoneDto PickerClimateZoneDtoMapping(
        this ClimateZone model) => new(
            Id: model.Id,
            Name: model.Name
        );
}
