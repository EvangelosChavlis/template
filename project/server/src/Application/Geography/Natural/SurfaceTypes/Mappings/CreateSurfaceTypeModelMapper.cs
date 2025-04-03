// source
using server.src.Domain.Geography.Natural.SurfaceTypes.Dtos;
using server.src.Domain.Geography.Natural.SurfaceTypes.Models;

namespace server.src.Application.Geography.Natural.SurfaceTypes.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="CreateSurfaceTypeDto"/> into a <see cref="SurfaceType"/> model.
/// This utility class is used to create new surface type instances based on provided data transfer objects.
/// </summary>
public static class CreateSurfaceTypeModelMapper
{
    /// <summary>
    /// Maps a <see cref="CreateSurfaceTypeDto"/> to a <see cref="SurfaceType"/> model, creating a new SurfaceType entity.
    /// </summary>
    /// <param name="dto">The data transfer object containing SurfaceType details.</param>
    /// <returns>A newly created <see cref="SurfaceType"/> model populated with data from the provided DTO.</returns>
    public static SurfaceType CreateSurfaceTypeModelMapping(this CreateSurfaceTypeDto dto)
        => new()
        {
            Name = dto.Name,
            Description = dto.Description,
            Code = dto.Code,
            IsActive = true,
            Version = Guid.NewGuid()
        };
}
