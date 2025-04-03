// source
using server.src.Domain.Geography.Natural.ClimateZones.Dtos;
using server.src.Domain.Geography.Natural.ClimateZones.Models;

namespace server.src.Application.Geography.Natural.ClimateZones.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="ClimateZone"/> model 
/// into a <see cref="ListItemClimateZoneDto"/>.
/// This utility class is used to transform climate zone data for list views with key details.
/// </summary>
public static class ListItemClimateZoneDtoMapper
{
    /// <summary>
    /// Maps a <see cref="ClimateZone"/> model to a <see cref="ListItemClimateZoneDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="ClimateZone"/> model that will be mapped.</param>
    /// <returns>A <see cref="ListItemClimateZoneDto"/> representing the climate zone model with essential details.</returns>
    public static ListItemClimateZoneDto ListItemClimateZoneDtoMapping(
        this ClimateZone model) => new(
            Id: model.Id,
            Name: model.Name,
            Code: model.Code,
            AvgTemperatureC: model.AvgTemperatureC,
            AvgPrecipitationMm: model.AvgPrecipitationMm,
            IsActive: model.IsActive
        );
}