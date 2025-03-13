// source
using server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;

namespace server.src.Application.Geography.Administrative.Neighborhoods.Mappings;

/// <summary>
/// Provides mapping functionality to convert an <see cref="ImportNeighborhoodDto"/> into a <see cref="CreateNeighborhoodDto"/>.
/// This utility class is used to transform imported neighborhood data into a format suitable for creating a new neighborhood entity.
/// </summary>
public static class ImportNeighborhoodModelMapper
{
    /// <summary>
    /// Maps an <see cref="ImportNeighborhoodDto"/> to a <see cref="CreateNeighborhoodDto"/>, 
    /// preparing it for neighborhood entity creation by adding the associated state ID.
    /// </summary>
    /// <param name="dto">The data transfer object containing imported neighborhood details.</param>
    /// <param name="districtId">The unique identifier of the state the neighborhood belongs to.</param>
    /// <returns>A new <see cref="CreateNeighborhoodDto"/> instance populated with data from the provided DTO.</returns>
    public static CreateNeighborhoodDto ImportNeighborhoodMapping(
        this ImportNeighborhoodDto dto, 
        Guid districtId
    ) => new (
        Name: dto.Name,
        Description: dto.Description,
        Population: dto.Population,
        AreaKm2: dto.AreaKm2,
        Zipcode: dto.Zipcode,
        Code: dto.Code,
        DistrictId: districtId
    );
}