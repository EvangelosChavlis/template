// source
using server.src.Domain.Geography.Administrative.Districts.Models;
using server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;

namespace server.src.Application.Geography.Administrative.Neighborhoods.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="CreateNeighborhoodDto"/> into a <see cref="Neighborhood"/> model.
/// This utility class is used to create new Neighborhood instances based on provided data transfer objects.
/// </summary>
public static class CreateNeighborhoodModelMapper
{
    /// <summary>
    /// Maps a <see cref="CreateNeighborhoodDto"/> to a <see cref="Neighborhood"/> model, creating a new Neighborhood entity.
    /// </summary>
    /// <param name="dto">The data transfer object containing Neighborhood details.</param>
    /// <returns>A newly created <see cref="Neighborhood"/> model populated with data from the provided DTO.</returns>
    public static Neighborhood CreateNeighborhoodModelMapping(
        this CreateNeighborhoodDto dto, 
        District model) => new()
        {
            Name = dto.Name,
            Description = dto.Description,
            Population = dto.Population,
            AreaKm2 = dto.AreaKm2,
            Zipcode = dto.Zipcode,
            Code = dto.Code,
            IsActive = true,
            Version = Guid.NewGuid(),
            District = model,
            DistrictId = model.Id
        };
}
