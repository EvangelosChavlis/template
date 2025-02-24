// source
using server.src.Domain.Geography.Natural.TerrainTypes.Dtos;

namespace server.src.Application.Geography.Natural.TerrainTypes.Mappings;

/// <summary>
/// Provides a method to generate an <see cref="ItemTerrainTypeDto"/> with default values  
/// to represent an error state when a valid terraintype cannot be retrieved.
/// </summary>
public class ErrorItemTerrainTypeDtoMapper
{
    /// <summary>
    /// Creates an <see cref="ItemTerrainTypeDto"/> with default values in case of an error.
    /// </summary>
    /// <returns>An <see cref="ItemTerrainTypeDto"/> with empty or placeholder values, indicating an error state.</returns>
    public static ItemTerrainTypeDto ErrorItemTerrainTypeDtoMapping() => 
        new (
            Id: Guid.Empty,
            Name: string.Empty,
            Description: string.Empty,
            IsActive: false,
            Version: Guid.Empty 
        );
}