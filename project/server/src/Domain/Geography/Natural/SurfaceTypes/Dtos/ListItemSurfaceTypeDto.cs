namespace server.src.Domain.Geography.Natural.SurfaceTypes.Dtos;

/// <summary>
/// DTO representing a surface type item in a list.
/// </summary>
/// <param name="Id">The unique identifier of the surface type.</param>
/// <param name="Name">The name of the surface type.</param>
/// <param name="Code">A unique code of the surface type.</param>
/// <param name="IsActive">Indicates whether the surface type is active.</param>
/// <param name="Count">The number of associated items.</param>
public record ListItemSurfaceTypeDto(
    Guid Id,
    string Name,
    string Code,
    bool IsActive,
    int Count
);