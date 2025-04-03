// source
using server.src.Domain.Geography.Natural.SurfaceTypes.Dtos;

namespace server.src.Application.Geography.Natural.SurfaceTypes.Mappings;

/// <summary>
/// Provides a method to generate an <see cref="ItemSurfaceTypeDto"/> with default values  
/// to represent an error state when a valid SurfaceType cannot be retrieved.
/// </summary>
public class ErrorItemSurfaceTypeDtoMapper
{
    /// <summary>
    /// Creates an <see cref="ItemSurfaceTypeDto"/> with default values in case of an error.
    /// </summary>
    /// <returns>An <see cref="ItemSurfaceTypeDto"/> with empty or placeholder values, indicating an error state.</returns>
    public static ItemSurfaceTypeDto ErrorItemSurfaceTypeDtoMapping() => 
        new (
            Id: Guid.Empty,
            Name: string.Empty,
            Description: string.Empty,
            Code: string.Empty,
            IsActive: false,
            Version: Guid.Empty 
        );
}