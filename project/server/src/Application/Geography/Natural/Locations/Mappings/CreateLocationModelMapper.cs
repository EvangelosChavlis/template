// source
using server.src.Domain.Geography.Natural.ClimateZones.Models;
using server.src.Domain.Geography.Natural.Locations.Dtos;
using server.src.Domain.Geography.Natural.Locations.Models;
using server.src.Domain.Geography.Natural.NaturalFeatures.Models;
using server.src.Domain.Geography.Natural.SurfaceTypes.Models;
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
    /// <param name="naturalFeatureModel">The associated <see cref="NaturalFeature"/> model.</param>
    /// <param name="surfaceTypeModel">The associated <see cref="SurfaceType"/> model.</param>
    /// <param name="timezoneModel">The associated <see cref="Timezone"/> model.</param>
    /// <returns>A newly created <see cref="Location"/> model populated with data from the provided DTO.</returns>
    public static Location CreateLocationModelMapping(
        this CreateLocationDto dto,
        ClimateZone climateZoneModel,
        NaturalFeature naturalFeatureModel,
        SurfaceType surfaceTypeModel,  
        Timezone timezoneModel)
    {
        return new Location
        {
            Longitude = dto.Longitude,
            Latitude = dto.Latitude,
            Altitude = dto.Altitude,
            Depth = dto.Depth,
            IsActive = true,
            TimezoneId = timezoneModel.Id,
            Timezone = timezoneModel,
            SurfaceTypeId = surfaceTypeModel.Id,
            SurfaceType = surfaceTypeModel,
            ClimateZoneId = climateZoneModel.Id,
            ClimateZone = climateZoneModel,
            NaturalFeature = naturalFeatureModel,
            NaturalFeatureId = naturalFeatureModel.Id,
            Version = Guid.NewGuid()
        };
    }
}