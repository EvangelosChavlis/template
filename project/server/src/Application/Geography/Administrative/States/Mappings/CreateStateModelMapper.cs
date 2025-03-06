// source
using server.src.Domain.Geography.Administrative.Countries.Models;
using server.src.Domain.Geography.Administrative.States.Dtos;
using server.src.Domain.Geography.Administrative.States.Models;

namespace server.src.Application.Geography.Administrative.States.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="CreateStateDto"/> into a <see cref="State"/> model.
/// This utility class is used to create new state instances based on provided data transfer objects.
/// </summary>
public static class CreateStateModelMapper
{
    /// <summary>
    /// Maps a <see cref="CreateStateDto"/> to a <see cref="State"/> model, creating a new state entity.
    /// </summary>
    /// <param name="dto">The data transfer object containing state details.</param>
    /// <returns>A newly created <see cref="State"/> model populated with data from the provided DTO.</returns>
    public static State CreateStateModelMapping(
        this CreateStateDto dto, 
        Country model) => new()
        {
            Name = dto.Name,
            Description = dto.Description,
            Capital = dto.Capital,
            Population = dto.Population,
            AreaKm2 = dto.AreaKm2,
            Code = dto.Code,
            IsActive = true,
            Version = Guid.NewGuid(),
            Country = model,
            CountryId = model.Id
        };
}
