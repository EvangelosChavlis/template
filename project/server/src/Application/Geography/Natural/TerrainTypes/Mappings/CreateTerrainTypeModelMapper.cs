// source
using server.src.Domain.Geography.Natural.TerrainTypes.Dtos;
using server.src.Domain.Geography.Natural.TerrainTypes.Models;

namespace server.src.Application.Geography.Natural.TerrainTypes.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="CreateTerrainTypeDto"/> into a <see cref="TerrainType"/> model.
/// This utility class is used to create new terrain type instances based on provided data transfer objects.
/// </summary>
public static class CreateTerrainTypeModelMapper
{
    /// <summary>
    /// Maps a <see cref="CreateTerrainTypeDto"/> to a <see cref="TerrainType"/> model, creating a new terraintype entity.
    /// </summary>
    /// <param name="dto">The data transfer object containing terraintype details.</param>
    /// <returns>A newly created <see cref="TerrainType"/> model populated with data from the provided DTO.</returns>
    public static TerrainType CreateTerrainTypeModelMapping(this CreateTerrainTypeDto dto)
        => new()
        {
            Name = dto.Name,
            Description = dto.Description,
            IsActive = true,
            Version = Guid.NewGuid()
        };
}
