namespace server.src.Domain.Geography.Natural.SurfaceTypes.Dtos;

/// <summary>
/// DTO for selecting a surface type.
/// </summary>
/// <param name="Id">The unique identifier of the surface type.</param>
/// <param name="Name">The name of the surface type.</param>
public record PickerSurfaceTypeDto(
    Guid Id,
    string Name
);