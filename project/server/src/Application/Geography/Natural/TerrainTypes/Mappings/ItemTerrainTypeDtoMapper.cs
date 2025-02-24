// source
using server.src.Domain.Geography.Natural.TerrainTypes.Dtos;
using server.src.Domain.Geography.Natural.TerrainTypes.Models;

namespace server.src.Application.Geography.Natural.TerrainTypes.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="TerrainType"/> model  
/// into an <see cref="ItemTerrainTypeDto"/> for detailed item representation.
/// </summary>
public static class ItemTerrainTypeDtoMapper
{
    /// <summary>
    /// Maps a TerrainType model to an ItemTerrainTypeDto.
    /// </summary>
    /// <param name="model">The TerrainType model that will be mapped to an ItemTerrainTypeDto.</param>
    /// <returns>An ItemTerrainTypeDto representing the TerrainType model with full details for an individual item view.</returns>
    public static ItemTerrainTypeDto ItemTerrainTypeDtoMapping(
        this TerrainType model) => new(
            Id: model.Id,
            Name: model.Name,
            Description: model.Description,
            IsActive: model.IsActive,
            Version: model.Version
        );
}