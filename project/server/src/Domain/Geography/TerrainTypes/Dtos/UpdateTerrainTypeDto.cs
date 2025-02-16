namespace server.src.Domain.Geography.TerrainTypes.Dtos;

public record UpdateTerrainTypeDto(
    string Name,
    string Description,
    Guid Version
);