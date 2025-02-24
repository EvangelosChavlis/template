namespace server.src.Domain.Geography.Natural.TerrainTypes.Dtos;

/// <summary>
/// DTO for selecting a terrain type.
/// </summary>
/// <param name="Id">The unique identifier of the terrain type.</param>
/// <param name="Name">The name of the terrain type.</param>
public record PickerTerrainTypeDto(
    Guid Id,
    string Name
);