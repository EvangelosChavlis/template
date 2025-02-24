namespace server.src.Domain.Geography.Natural.TerrainTypes.Dtos;

/// <summary>
/// DTO representing a terrain type item in a list.
/// </summary>
/// <param name="Id">The unique identifier of the terrain type.</param>
/// <param name="Name">The name of the terrain type.</param>
/// <param name="Description">A brief description of the terrain type.</param>
/// <param name="IsActive">Indicates whether the terrain type is active.</param>
/// <param name="Count">The number of associated items.</param>
public record ListItemTerrainTypeDto(
    Guid Id,
    string Name,
    string Description,
    bool IsActive,
    int Count
);