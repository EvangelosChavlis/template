// source
using server.src.Domain.Geography.Administrative.Municipalities.Models;
using server.src.Domain.Geography.Administrative.Municipalities.Dtos;
using server.src.Domain.Geography.Administrative.Regions.Models;

namespace server.src.Application.Geography.Administrative.Municipalities.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="CreateMunicipalityDto"/> into a <see cref="Municipality"/> model.
/// This utility class is used to create new municipality instances based on provided data transfer objects.
/// </summary>
public static class CreateMunicipalityModelMapper
{
    /// <summary>
    /// Maps a <see cref="CreateMunicipalityDto"/> to a <see cref="Municipality"/> model, creating a new municipality entity.
    /// </summary>
    /// <param name="dto">The data transfer object containing municipality details.</param>
    /// <returns>A newly created <see cref="Municipality"/> model populated with data from the provided DTO.</returns>
    public static Municipality CreateMunicipalityModelMapping(
        this CreateMunicipalityDto dto, 
        Region model) => new()
        {
            Name = dto.Name,
            Description = dto.Description,
            AreaKm2 = dto.AreaKm2,
            Population = dto.Population,
            Code = dto.Code,
            IsActive = true,
            Version = Guid.NewGuid(),
            Region = model,
            RegionId = model.Id
        };
}
