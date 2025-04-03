// source
using server.src.Domain.Geography.Administrative.Continents.Dtos;
using server.src.Domain.Geography.Administrative.Continents.Models;

namespace server.src.Application.Geography.Administrative.Continents.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="CreateContinentDto"/> into a <see cref="Continent"/> model.
/// This utility class is used to create new surface type instances based on provided data transfer objects.
/// </summary>
public static class CreateContinentModelMapper
{
    /// <summary>
    /// Maps a <see cref="CreateContinentDto"/> to a <see cref="Continent"/> model, creating a new continent entity.
    /// </summary>
    /// <param name="dto">The data transfer object containing continent details.</param>
    /// <returns>A newly created <see cref="Continent"/> model populated with data from the provided DTO.</returns>
    public static Continent CreateContinentModelMapping(this CreateContinentDto dto)
        => new()
        {
            Name = dto.Name,
            Description = dto.Description,
            Code = dto.Code,
            AreaKm2 = dto.AreaKm2,
            Population = dto.Population,
            IsActive = true,
            Version = Guid.NewGuid()
        };
}
