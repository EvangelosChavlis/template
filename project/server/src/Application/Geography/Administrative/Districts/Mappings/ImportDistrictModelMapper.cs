// source
using server.src.Domain.Geography.Administrative.Districts.Dtos;

namespace server.src.Application.Geography.Administrative.Districts.Mappings;

/// <summary>
/// Provides mapping functionality to convert an <see cref="ImportDistrictDto"/> into a <see cref="CreateDistrictDto"/>.
/// This utility class is used to transform imported district data into a format suitable for creating a new district entity.
/// </summary>
public static class ImportDistrictModelMapper
{
    /// <summary>
    /// Maps an <see cref="ImportDistrictDto"/> to a <see cref="CreateDistrictDto"/>, 
    /// preparing it for district entity creation by adding the associated state ID.
    /// </summary>
    /// <param name="dto">The data transfer object containing imported district details.</param>
    /// <param name="municipalityId">The unique identifier of the state the district belongs to.</param>
    /// <returns>A new <see cref="CreateDistrictDto"/> instance populated with data from the provided DTO.</returns>
    public static CreateDistrictDto ImportDistrictMapping(
        this ImportDistrictDto dto, 
        Guid municipalityId
    ) => new (
        Name: dto.Name,
        Description: dto.Description,
        AreaKm2: dto.AreaKm2,
        Population: dto.Population,
        Code: dto.Code,
        MunicipalityId: municipalityId
    );
}