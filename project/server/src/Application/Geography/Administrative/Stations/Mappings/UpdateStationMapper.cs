// source
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;
using server.src.Domain.Geography.Administrative.Stations.Dtos;
using server.src.Domain.Geography.Administrative.Stations.Models;
using server.src.Domain.Geography.Natural.Locations.Models;

namespace server.src.Application.Geography.Administrative.Stations.Mappings;

/// <summary>
/// Provides mapping functionality to update an existing <see cref="Station"/> model 
/// using data from an <see cref="UpdateStationDto"/>.
/// This utility class ensures that the country entity is updated efficiently with new details.
/// </summary>
public static class UpdateStationMapper
{
    /// <summary>
    /// Updates an existing <see cref="Station"/> model with data from an <see cref="UpdateStationDto"/>.
    /// </summary>
    /// <param name="dto">The data transfer object containing updated country details.</param>
    /// <param name="modelStation">The existing <see cref="Station"/> model to be updated.</param>
    public static void UpdateStationMapping(
        this UpdateStationDto dto, 
        Station modelStation,
        Location locationModel, 
        Neighborhood? neighborhoodModel
    )
    {
        modelStation.Name = dto.Name;
        modelStation.Description = dto.Description;
        modelStation.Code = dto.Code;
        modelStation.IsActive = modelStation.IsActive;
        modelStation.Location = locationModel;
        modelStation.LocationId = locationModel.Id;
        modelStation.Neighborhood = neighborhoodModel;
        modelStation.NeighborhoodId = neighborhoodModel?.Id;
        modelStation.Version = Guid.NewGuid();
    }
}