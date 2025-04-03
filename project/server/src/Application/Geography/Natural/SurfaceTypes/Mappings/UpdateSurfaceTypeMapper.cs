// source
using server.src.Domain.Geography.Natural.SurfaceTypes.Dtos;
using server.src.Domain.Geography.Natural.SurfaceTypes.Models;

namespace server.src.Application.Geography.Natural.SurfaceTypes.Mappings;

/// <summary>
/// Provides mapping functionality to update an existing <see cref="SurfaceType"/> model 
/// using data from an <see cref="UpdateSurfaceTypeDto"/>.
/// This utility class ensures that the SurfaceType entity is updated efficiently with new details.
/// </summary>
public static class UpdateSurfaceTypeMapper
{
    /// <summary>
    /// Updates an existing <see cref="SurfaceType"/> model with data from an <see cref="UpdateSurfaceTypeDto"/>.
    /// </summary>
    /// <param name="dto">The data transfer object containing updated SurfaceType details.</param>
    /// <param name="model">The existing <see cref="SurfaceType"/> model to be updated.</param>
    public static void UpdateSurfaceTypeMapping(this UpdateSurfaceTypeDto dto, SurfaceType model)
    {
        model.Name = dto.Name;
        model.Description = dto.Description;
        model.Code = dto.Code;
        model.IsActive = model.IsActive;
        model.Version = Guid.NewGuid();
    }
}
