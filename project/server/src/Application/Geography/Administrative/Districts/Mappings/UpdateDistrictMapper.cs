// source
using server.src.Domain.Geography.Administrative.Districts.Dtos;
using server.src.Domain.Geography.Administrative.Districts.Models;
using server.src.Domain.Geography.Administrative.Municipalities.Models;

namespace server.src.Application.Geography.Administrative.Districts.Mappings;

/// <summary>
/// Provides mapping functionality to update an existing <see cref="District"/> model 
/// using data from an <see cref="UpdateDistrictDto"/>.
/// This utility class ensures that the country entity is updated efficiently with new details.
/// </summary>
public static class UpdateDistrictMapper
{
    /// <summary>
    /// Updates an existing <see cref="District"/> model with data from an <see cref="UpdateDistrictDto"/>.
    /// </summary>
    /// <param name="dto">The data transfer object containing updated country details.</param>
    /// <param name="modelDistrict">The existing <see cref="District"/> model to be updated.</param>
    public static void UpdateDistrictMapping(
        this UpdateDistrictDto dto, 
        District modelDistrict,
        Municipality modelMunicipality
    )
    {
        modelDistrict.Name = dto.Name;
        modelDistrict.Description = dto.Description;
        modelDistrict.Population = dto.Population;
        modelDistrict.AreaKm2 = dto.AreaKm2;
        modelDistrict.Code = dto.Code;
        modelDistrict.IsActive = modelDistrict.IsActive;
        modelDistrict.Municipality = modelMunicipality;
        modelDistrict.MunicipalityId = modelMunicipality.Id;
        modelDistrict.Version = Guid.NewGuid();
    }
}