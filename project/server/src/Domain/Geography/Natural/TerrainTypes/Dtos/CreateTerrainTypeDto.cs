namespace server.src.Domain.Geography.Natural.TerrainTypes.Dtos;

/// <summary>
/// DTO for creating a new terrain type.
/// </summary>
/// <param name="Name">The name of the terrain type.</param>
/// <param name="Description">A brief description of the terrain type.</param>
public record CreateTerrainTypeDto(
    string Name,
    string Description
);