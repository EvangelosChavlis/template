// source
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;
using server.src.Domain.Geography.Administrative.Stations.Dtos;
using server.src.Domain.Geography.Administrative.Stations.Models;
using server.src.Domain.Geography.Natural.Locations.Models;

namespace server.src.Application.Geography.Administrative.Stations.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="CreateStationDto"/> into a <see cref="Station"/> model.
/// This utility class is used to create new station instances based on provided data transfer objects.
/// </summary>
public static class CreateStationModelMapper
{
    /// <summary>
    /// Maps a <see cref="CreateStationDto"/> to a <see cref="Station"/> model, creating a new station entity.
    /// </summary>
    /// <param name="dto">The data transfer object containing station details.</param>
    /// <returns>A newly created <see cref="Station"/> model populated with data from the provided DTO.</returns>
    public static Station CreateStationModelMapping(
        this CreateStationDto dto, 
        Location locationModel, 
        Neighborhood? neighborhoodModel) => new()
        {
            Name = dto.Name,
            Description = dto.Description,
            Code = dto.Code,
            IsActive = true,
            Location = locationModel,
            LocationId = locationModel.Id,
            Neighborhood = neighborhoodModel,
            NeighborhoodId = neighborhoodModel?.Id,
            Version = Guid.NewGuid()
        };
}