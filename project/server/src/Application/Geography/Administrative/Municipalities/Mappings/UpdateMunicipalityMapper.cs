// source
using server.src.Domain.Geography.Administrative.Municipalities.Dtos;
using server.src.Domain.Geography.Administrative.Municipalities.Models;
using server.src.Domain.Geography.Administrative.Regions.Models;

namespace server.src.Application.Geography.Administrative.Municipalities.Mappings;

/// <summary>
/// Provides mapping functionality to update an existing <see cref="Municipality"/> model 
/// using data from an <see cref="UpdateMunicipalityDto"/>.
/// This utility class ensures that the country entity is updated efficiently with new details.
/// </summary>
public static class UpdateMunicipalityMapper
{
    /// <summary>
    /// Updates an existing <see cref="Municipality"/> model with data from an <see cref="UpdateMunicipalityDto"/>.
    /// </summary>
    /// <param name="dto">The data transfer object containing updated country details.</param>
    /// <param name="modelMunicipality">The existing <see cref="Municipality"/> model to be updated.</param>
    public static void UpdateMunicipalityMapping(
        this UpdateMunicipalityDto dto, 
        Municipality modelMunicipality,
        Region modelRegion
    )
    {
        modelMunicipality.Name = dto.Name;
        modelMunicipality.Description = dto.Description;
        modelMunicipality.Population = dto.Population;
        modelMunicipality.AreaKm2 = dto.AreaKm2;
        modelMunicipality.Code = dto.Code;
        modelMunicipality.IsActive = modelMunicipality.IsActive;
        modelMunicipality.Region = modelRegion;
        modelMunicipality.RegionId = modelRegion.Id;
        modelMunicipality.Version = Guid.NewGuid();
    }
}