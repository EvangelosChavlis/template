// source
using server.src.Domain.Geography.Administrative.Regions.Dtos;
using server.src.Domain.Geography.Administrative.Regions.Models;
using server.src.Domain.Geography.Administrative.States.Models;

namespace server.src.Application.Geography.Administrative.Regions.Mappings;

/// <summary>
/// Provides mapping functionality to update an existing <see cref="Region"/> model 
/// using data from an <see cref="UpdateRegionDto"/>.
/// This utility class ensures that the country entity is updated efficiently with new details.
/// </summary>
public static class UpdateRegionMapper
{
    /// <summary>
    /// Updates an existing <see cref="Region"/> model with data from an <see cref="UpdateRegionDto"/>.
    /// </summary>
    /// <param name="dto">The data transfer object containing updated country details.</param>
    /// <param name="modelRegion">The existing <see cref="Region"/> model to be updated.</param>
    public static void UpdateRegionMapping(
        this UpdateRegionDto dto, 
        Region modelRegion,
        State modelState
    )
    {
        modelRegion.Name = dto.Name;
        modelRegion.Description = dto.Description;
        modelRegion.AreaKm2 = dto.AreaKm2;
        modelRegion.Code = dto.Code;
        modelRegion.IsActive = modelRegion.IsActive;
        modelRegion.State = modelState;
        modelRegion.StateId = modelState.Id;
        modelRegion.Version = Guid.NewGuid();
    }
}