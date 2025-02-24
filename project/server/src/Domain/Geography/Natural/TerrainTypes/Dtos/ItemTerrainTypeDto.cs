namespace server.src.Domain.Geography.Natural.TerrainTypes.Dtos;

/// <summary>
/// DTO representing a terrain type item.
/// </summary>
/// <param name="Id">The unique identifier of the terrain type.</param>
/// <param name="Name">The name of the terrain type.</param>
/// <param name="Description">A brief description of the terrain type.</param>
/// <param name="IsActive">Indicates whether the terrain type is active.</param>
/// <param name="Version">The version identifier for concurrency control.</param>
public record ItemTerrainTypeDto(
    Guid Id,
    string Name,
    string Description,
    bool IsActive,
    Guid Version
);