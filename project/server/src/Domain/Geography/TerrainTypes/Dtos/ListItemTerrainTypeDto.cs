namespace Domain.Geography.TerrainTypes.Dtos;

public record ListItemTerrainTypeDto(
    Guid Id,
    string Name,
    string Description,
    bool IsActive
);