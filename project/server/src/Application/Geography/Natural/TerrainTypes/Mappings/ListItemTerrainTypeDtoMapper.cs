// source
using server.src.Domain.Geography.Natural.TerrainTypes.Dtos;
using server.src.Domain.Geography.Natural.TerrainTypes.Models;

namespace server.src.Application.Geography.Natural.TerrainTypes.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="TerrainType"/> model 
/// into a <see cref="ListItemTerrainTypeDto"/>.
/// This utility class is used to transform terraintype data for list views with key details.
/// </summary>
public static class ListItemTerrainTypeDtoMapper
{
    /// <summary>
    /// Maps a <see cref="TerrainType"/> model to a <see cref="ListItemTerrainTypeDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="TerrainType"/> model that will be mapped.</param>
    /// <returns>A <see cref="ListItemTerrainTypeDto"/> representing the terraintype model with essential details.</returns>
    public static ListItemTerrainTypeDto ListItemTerrainTypeDtoMapping(
        this TerrainType model) => new(
            Id: model.Id,
            Name: model.Name,
            Description: model.Description,
            IsActive: model.IsActive,
            Count: model.Locations.Count
        );
}