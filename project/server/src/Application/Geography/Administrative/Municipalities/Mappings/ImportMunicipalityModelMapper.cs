// source
using server.src.Domain.Geography.Administrative.Municipalities.Dtos;

namespace server.src.Application.Geography.Administrative.Municipalities.Mappings;

/// <summary>
/// Provides mapping functionality to convert an <see cref="ImportMunicipalityDto"/> into a <see cref="CreateMunicipalityDto"/>.
/// This utility class is used to transform imported municipality data into a format suitable for creating a new municipality entity.
/// </summary>
public static class ImportMunicipalityModelMapper
{
    /// <summary>
    /// Maps an <see cref="ImportMunicipalityDto"/> to a <see cref="CreateMunicipalityDto"/>, 
    /// preparing it for municipality entity creation by adding the associated state ID.
    /// </summary>
    /// <param name="dto">The data transfer object containing imported municipality details.</param>
    /// <param name="regionId">The unique identifier of the state the municipality belongs to.</param>
    /// <returns>A new <see cref="CreateMunicipalityDto"/> instance populated with data from the provided DTO.</returns>
    public static CreateMunicipalityDto ImportMunicipalityMapping(
        this ImportMunicipalityDto dto, 
        Guid regionId
    ) => new (
        Name: dto.Name,
        Description: dto.Description,
        AreaKm2: dto.AreaKm2,
        Population: dto.Population,
        Code: dto.Code,
        RegionId: regionId
    );
}