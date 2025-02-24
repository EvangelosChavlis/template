// source
using server.src.Domain.Geography.Natural.ClimateZones.Models;
using server.src.Domain.Geography.Natural.Locations.Dtos;
using server.src.Domain.Geography.Natural.Locations.Models;
using server.src.Domain.Geography.Natural.TerrainTypes.Models;
using server.src.Domain.Geography.Natural.Timezones.Models;

namespace server.src.Application.Geography.Natural.Locations.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="CreateLocationDto"/> into a <see cref="Location"/> model.  
/// This utility class is used to create new location entities based on provided data transfer objects.
/// </summary>
public static class CreateLocationModelMapper
{
    /// <summary>
    /// Maps a <see cref="CreateLocationDto"/> to a <see cref="Location"/> model, creating a new location entity.  
    /// </summary>
    /// <param name="dto">The data transfer object containing location details.</param>
    /// <param name="climateZoneModel">The associated <see cref="ClimateZone"/> model.</param>
    /// <param name="terrainTypeModel">The associated <see cref="TerrainType"/> model.</param>
    /// <param name="timezoneModel">The associated <see cref="Timezone"/> model.</param>
    /// <returns>A newly created <see cref="Location"/> model populated with data from the provided DTO.</returns>
    public static Location CreateLocationModelMapping(
        this CreateLocationDto dto,
        ClimateZone climateZoneModel,
        TerrainType terrainTypeModel,  
        Timezone timezoneModel)
    {
        return new Location
        {
            Longitude = dto.Longitude,
            Latitude = dto.Latitude,
            Altitude = dto.Altitude,
            IsActive = true,
            TimezoneId = timezoneModel.Id,
            Timezone = timezoneModel,
            TerrainTypeId = terrainTypeModel.Id,
            TerrainType = terrainTypeModel,
            ClimateZoneId = climateZoneModel.Id,
            ClimateZone = climateZoneModel,
            Version = Guid.NewGuid()
        };
    }
}