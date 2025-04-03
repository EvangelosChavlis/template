namespace server.src.Domain.Geography.Natural.SurfaceTypes.Dtos;

/// <summary>
/// DTO for updating an existing surface type.
/// </summary>
/// <param name="Name">The updated name of the surface type.</param>
/// <param name="Description">The updated description of the surface type.</param>
/// <param name="Code">A brief code of the surface type.</param>
/// <param name="Version">The version identifier for concurrency control.</param>
public record UpdateSurfaceTypeDto(
    string Name,
    string Description,
    string Code,
    Guid Version
);