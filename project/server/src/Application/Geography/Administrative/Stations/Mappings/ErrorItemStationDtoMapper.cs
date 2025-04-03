// source
using server.src.Domain.Geography.Administrative.Stations.Dtos;

namespace server.src.Application.Geography.Administrative.Stations.Mappings;

/// <summary>
/// Provides a method to generate an <see cref="ItemStationDto"/> with default values  
/// to represent an error station when a valid Station cannot be retrieved.
/// </summary>
public class ErrorItemStationDtoMapper
{
    /// <summary>
    /// Creates an <see cref="ItemStationDto"/> with default values in case of an error.
    /// </summary>
    /// <returns>An <see cref="ItemStationDto"/> with empty or placeholder values, indicating an error station.</returns>
    public static ItemStationDto ErrorItemStationDtoMapping() => 
        new (
            Id: Guid.Empty,
            Name: string.Empty,
            Description: string.Empty,
            Code: string.Empty,
            IsActive: false,
            Location: string.Empty,
            Version: Guid.Empty
        );
}
