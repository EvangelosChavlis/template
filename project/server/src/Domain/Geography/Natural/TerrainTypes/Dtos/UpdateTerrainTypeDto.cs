namespace server.src.Domain.Geography.Natural.TerrainTypes.Dtos;

/// <summary>
/// DTO for updating an existing terrain type.
/// </summary>
/// <param name="Name">The updated name of the terrain type.</param>
/// <param name="Description">The updated description of the terrain type.</param>
/// <param name="Version">The version identifier for concurrency control.</param>
public record UpdateTerrainTypeDto(
    string Name,
    string Description,
    Guid Version
);