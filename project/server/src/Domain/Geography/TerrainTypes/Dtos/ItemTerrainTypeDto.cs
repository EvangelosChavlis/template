namespace server.src.Domain.Geography.TerrainTypes.Dtos;

public record ItemTerrainTypeDto(
    Guid Id,
    string Name,
    string Description,
    bool IsActive,
    Guid Version
);