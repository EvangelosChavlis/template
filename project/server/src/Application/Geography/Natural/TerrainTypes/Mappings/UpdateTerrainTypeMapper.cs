// source
using server.src.Domain.Geography.Natural.TerrainTypes.Dtos;
using server.src.Domain.Geography.Natural.TerrainTypes.Models;

namespace server.src.Application.Geography.Natural.TerrainTypes.Mappings;

/// <summary>
/// Provides mapping functionality to update an existing <see cref="TerrainType"/> model 
/// using data from an <see cref="UpdateTerrainTypeDto"/>.
/// This utility class ensures that the terraintype entity is updated efficiently with new details.
/// </summary>
public static class UpdateTerrainTypeMapper
{
    /// <summary>
    /// Updates an existing <see cref="TerrainType"/> model with data from an <see cref="UpdateTerrainTypeDto"/>.
    /// </summary>
    /// <param name="dto">The data transfer object containing updated terraintype details.</param>
    /// <param name="model">The existing <see cref="TerrainType"/> model to be updated.</param>
    public static void UpdateTerrainTypeMapping(this UpdateTerrainTypeDto dto, TerrainType model)
    {
        model.Name = dto.Name;
        model.Description = dto.Description;
        model.Version = Guid.NewGuid();
    }
}
