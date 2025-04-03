// source
using server.src.Domain.Geography.Natural.ClimateZones.Dtos;
using server.src.Domain.Geography.Natural.ClimateZones.Models;

namespace server.src.Application.Geography.Natural.ClimateZones.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="ClimateZone"/> model  
/// into an <see cref="ItemClimateZoneDto"/> for detailed item representation.
/// </summary>
public static class ItemClimateZoneDtoMapper
{
    /// <summary>
    /// Maps a ClimateZone model to an ItemClimateZoneDto.
    /// </summary>
    /// <param name="model">The ClimateZone model that will be mapped to an ItemClimateZoneDto.</param>
    /// <returns>An ItemClimateZoneDto representing the ClimateZone model with full details for an individual item view.</returns>
    public static ItemClimateZoneDto ItemClimateZoneDtoMapping(
        this ClimateZone model) => new(
            Id: model.Id,
            Name: model.Name,
            Description: model.Description,
            Code: model.Code,
            AvgTemperatureC: model.AvgTemperatureC,
            AvgPrecipitationMm: model.AvgPrecipitationMm,
            IsActive: model.IsActive,
            Version: model.Version
        );
}