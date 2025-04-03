namespace server.src.Domain.Geography.Natural.SurfaceTypes.Dtos;

/// <summary>
/// DTO for creating a new surface type.
/// </summary>
/// <param name="Name">The name of the surface type.</param>
/// <param name="Description">A brief description of the surface type.</param>
/// <param name="Code">A brief code of the surface type.</param>
public record CreateSurfaceTypeDto(
    string Name,
    string Description,
    string Code
);