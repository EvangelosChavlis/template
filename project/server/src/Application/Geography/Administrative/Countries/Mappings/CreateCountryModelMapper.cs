// source
using server.src.Domain.Geography.Administrative.Continents.Models;
using server.src.Domain.Geography.Administrative.Countries.Dtos;
using server.src.Domain.Geography.Administrative.Countries.Models;

namespace server.src.Application.Geography.Administrative.Countries.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="CreateCountryDto"/> into a <see cref="Country"/> model.
/// This utility class is used to create new terrain type instances based on provided data transfer objects.
/// </summary>
public static class CreateCountryModelMapper
{
    /// <summary>
    /// Maps a <see cref="CreateCountryDto"/> to a <see cref="Country"/> model, creating a new country entity.
    /// </summary>
    /// <param name="dto">The data transfer object containing country details.</param>
    /// <returns>A newly created <see cref="Country"/> model populated with data from the provided DTO.</returns>
    public static Country CreateCountryModelMapping(
        this CreateCountryDto dto, 
        Continent model) => new()
        {
            Name = dto.Name,
            Description = dto.Description,
            IsoCode = dto.IsoCode,
            Capital = dto.Capital,
            Population = dto.Population,
            AreaKm2 = dto.AreaKm2,
            IsActive = true,
            Version = Guid.NewGuid(),
            Continent = model,
            ContinentId = model.Id
        };
}
