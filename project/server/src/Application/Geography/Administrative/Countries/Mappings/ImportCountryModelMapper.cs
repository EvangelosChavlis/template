// source
using server.src.Domain.Geography.Administrative.Countries.Dtos;

namespace server.src.Application.Geography.Administrative.Countries.Mappings;

/// <summary>
/// Provides mapping functionality to convert an <see cref="ImportCountryDto"/> into a <see cref="CreateCountryDto"/>.
/// This utility class is used to transform imported country data into a format suitable for creating a new country entity.
/// </summary>
public static class ImportCountryModelMapper
{
    /// <summary>
    /// Maps an <see cref="ImportCountryDto"/> to a <see cref="CreateCountryDto"/>, 
    /// preparing it for country entity creation by adding the associated continent ID.
    /// </summary>
    /// <param name="dto">The data transfer object containing imported country details.</param>
    /// <param name="ContinentId">The unique identifier of the continent the country belongs to.</param>
    /// <returns>A new <see cref="CreateCountryDto"/> instance populated with data from the provided DTO.</returns>
    public static CreateCountryDto ImportCountryMapping(
        this ImportCountryDto dto, 
        Guid ContinentId
    ) => new (
        Name: dto.Name,
        Description: dto.Description,
        Code: dto.Code,
        Capital: dto.Capital,
        Population: dto.Population,
        AreaKm2: dto.AreaKm2,
        IsActive: true,
        ContinentId: ContinentId);
}
