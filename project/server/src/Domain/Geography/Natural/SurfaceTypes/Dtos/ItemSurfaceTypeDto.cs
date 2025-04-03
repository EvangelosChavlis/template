namespace server.src.Domain.Geography.Natural.SurfaceTypes.Dtos;

/// <summary>
/// DTO representing a surface type item.
/// </summary>
/// <param name="Id">The unique identifier of the surface type.</param>
/// <param name="Name">The name of the surface type.</param>
/// <param name="Description">A brief description of the surface type.</param>
/// <param name="Code">A unique code of the surface type.</param>
/// <param name="IsActive">Indicates whether the surface type is active.</param>
/// <param name="Version">The version identifier for concurrency control.</param>
public record ItemSurfaceTypeDto(
    Guid Id,
    string Name,
    string Description,
    string Code,
    bool IsActive,
    Guid Version
);