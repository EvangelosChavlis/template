// source
using server.src.Domain.Geography.Administrative.Districts.Dtos;
using server.src.Domain.Geography.Administrative.Districts.Models;
using server.src.Domain.Geography.Administrative.Municipalities.Models;

namespace server.src.Application.Geography.Administrative.Districts.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="CreateDistrictDto"/> into a <see cref="District"/> model.
/// This utility class is used to create new district instances based on provided data transfer objects.
/// </summary>
public static class CreateDistrictModelMapper
{
    /// <summary>
    /// Maps a <see cref="CreateDistrictDto"/> to a <see cref="District"/> model, creating a new district entity.
    /// </summary>
    /// <param name="dto">The data transfer object containing district details.</param>
    /// <returns>A newly created <see cref="District"/> model populated with data from the provided DTO.</returns>
    public static District CreateDistrictModelMapping(
        this CreateDistrictDto dto, 
        Municipality model) => new()
        {
            Name = dto.Name,
            Description = dto.Description,
            Population = dto.Population,
            AreaKm2 = dto.AreaKm2,
            Code = dto.Code,
            IsActive = true,
            Version = Guid.NewGuid(),
            Municipality = model,
            MunicipalityId = model.Id
        };
}
