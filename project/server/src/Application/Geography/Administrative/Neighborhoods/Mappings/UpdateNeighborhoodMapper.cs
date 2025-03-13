// source
using server.src.Domain.Geography.Administrative.Districts.Models;
using server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;

namespace server.src.Application.Geography.Administrative.Neighborhoods.Mappings;

/// <summary>
/// Provides mapping functionality to update an existing <see cref="Neighborhood"/> model 
/// using data from an <see cref="UpdateNeighborhoodDto"/>.
/// This utility class ensures that the country entity is updated efficiently with new details.
/// </summary>
public static class UpdateNeighborhoodMapper
{
    /// <summary>
    /// Updates an existing <see cref="Neighborhood"/> model with data from an <see cref="UpdateNeighborhoodDto"/>.
    /// </summary>
    /// <param name="dto">The data transfer object containing updated country details.</param>
    /// <param name="modelNeighborhood">The existing <see cref="Neighborhood"/> model to be updated.</param>
    public static void UpdateNeighborhoodMapping(
        this UpdateNeighborhoodDto dto, 
        Neighborhood modelNeighborhood,
        District modelDistrict
    )
    {
        modelNeighborhood.Name = dto.Name;
        modelNeighborhood.Description = dto.Description;
        modelNeighborhood.Population = dto.Population;
        modelNeighborhood.AreaKm2 = dto.AreaKm2;
        modelNeighborhood.Zipcode = dto.Zipcode;
        modelNeighborhood.Code = dto.Code; 
        modelNeighborhood.IsActive = modelNeighborhood.IsActive;
        modelNeighborhood.District = modelDistrict;
        modelNeighborhood.DistrictId = modelDistrict.Id;
        modelNeighborhood.Version = Guid.NewGuid();
    }
}