// source
using server.src.Domain.Geography.Natural.TerrainTypes.Dtos;
using server.src.Domain.Geography.Natural.TerrainTypes.Models;

namespace server.src.Application.Geography.Natural.TerrainTypes.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="TerrainType"/> model 
/// into a <see cref="PickerTerrainTypeDto"/>.
/// This mapper is used to transform terraintype data for selection lists or dropdowns.
/// </summary>
public static class PickerTerrainTypeDtoMapper
{
    /// <summary>
    /// Maps a <see cref="TerrainType"/> model to a <see cref="PickerTerrainTypeDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="TerrainType"/> model that will be mapped.</param>
    /// <returns>A <see cref="PickerTerrainTypeDto"/> containing essential details for selection purposes.</returns>
    public static PickerTerrainTypeDto PickerTerrainTypeDtoMapping(
        this TerrainType model) => new(
            Id: model.Id,
            Name: model.Name
        );
}
