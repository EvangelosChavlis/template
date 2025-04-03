// source
using server.src.Domain.Geography.Administrative.Stations.Dtos;

namespace server.src.Application.Geography.Administrative.Stations.Mappings;

/// <summary>
/// Provides mapping functionality to convert an <see cref="ImportStationDto"/> into a <see cref="CreateStationDto"/>.
/// This utility class is used to transform imported station data into a format suitable for creating a new station entity.
/// </summary>
public static class ImportStationModelMapper
{
    /// <summary>
    /// Maps an <see cref="ImportStationDto"/> to a <see cref="CreateStationDto"/>, 
    /// preparing it for station entity creation by adding the associated state ID.
    /// </summary>
    /// <param name="dto">The data transfer object containing imported station details.</param>
    /// <param name="neighborhoodId">The unique identifier of the neighborhood the station belongs to.</param>
    /// <returns>A new <see cref="CreateStationDto"/> instance populated with data from the provided DTO.</returns>
    public static CreateStationDto ImportStationMapping(
        this ImportStationDto dto, 
        Guid neighborhoodId
    ) => new (
        Name: dto.Name,
        Description: dto.Description,
        Longitude: dto.Longitude,
        Latitude: dto.Latitude,
        Code: dto.Code,
        NeighborhoodId: neighborhoodId
    );
}