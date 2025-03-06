// source
using server.src.Domain.Geography.Administrative.Regions.Dtos;
using server.src.Domain.Geography.Administrative.Regions.Models;
using server.src.Domain.Geography.Administrative.States.Models;

namespace server.src.Application.Geography.Administrative.Regions.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="CreateRegionDto"/> into a <see cref="Region"/> model.
/// This utility class is used to create new region instances based on provided data transfer objects.
/// </summary>
public static class CreateRegionModelMapper
{
    /// <summary>
    /// Maps a <see cref="CreateRegionDto"/> to a <see cref="Region"/> model, creating a new region entity.
    /// </summary>
    /// <param name="dto">The data transfer object containing region details.</param>
    /// <returns>A newly created <see cref="Region"/> model populated with data from the provided DTO.</returns>
    public static Region CreateRegionModelMapping(
        this CreateRegionDto dto, 
        State model) => new()
        {
            Name = dto.Name,
            Description = dto.Description,
            AreaKm2 = dto.AreaKm2,
            Code = dto.Code,
            IsActive = true,
            Version = Guid.NewGuid(),
            State = model,
            StateId = model.Id
        };
}
