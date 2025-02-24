// source
using server.src.Domain.Geography.Natural.ClimateZones.Models;
using server.src.Domain.Geography.Natural.Locations.Dtos;
using server.src.Domain.Geography.Natural.Locations.Models;
using server.src.Domain.Geography.Natural.TerrainTypes.Models;
using server.src.Domain.Geography.Natural.Timezones.Models;

namespace server.src.Application.Geography.Natural.Locations.Mappings;

/// <summary>
/// Provides mapping functionality to update an existing <see cref="Location"/> model  
/// using data from an <see cref="UpdateLocationDto"/>.  
/// Ensures that the location entity is updated efficiently while maintaining version control.
/// </summary>
public static class UpdateLocationMapper
{
    /// <summary>
    /// Updates an existing <see cref="Location"/> model with data from an <see cref="UpdateLocationDto"/>.  
    /// </summary>
    /// <param name="dto">The data transfer object containing updated location details.</param>
    /// <param name="locationModel">The existing <see cref="Location"/> model to be updated.</param>
    public static void UpdateLocationMapping(
        this UpdateLocationDto dto, 
        Location locationModel,
        ClimateZone climateZoneModel,
        TerrainType terrainTypeModel, 
        Timezone timezoneModel)
    {
        locationModel.Longitude = dto.Longitude;
        locationModel.Latitude = dto.Latitude;
        locationModel.Altitude = dto.Altitude;
        locationModel.IsActive = locationModel.IsActive;
        locationModel.ClimateZoneId = climateZoneModel.Id;
        locationModel.ClimateZone = climateZoneModel;
        locationModel.TerrainTypeId = terrainTypeModel.Id;
        locationModel.TerrainType = terrainTypeModel;
        locationModel.TimezoneId = timezoneModel.Id;
        locationModel.Timezone = timezoneModel;
        locationModel.Version = Guid.NewGuid();
    }
}
