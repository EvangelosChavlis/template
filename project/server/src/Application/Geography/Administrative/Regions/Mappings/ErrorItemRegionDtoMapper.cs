// source
using server.src.Domain.Geography.Administrative.Regions.Dtos;

namespace server.src.Application.Geography.Administrative.Regions.Mappings;

/// <summary>
/// Provides a method to generate an <see cref="ItemRegionDto"/> with default values  
/// to represent an error region when a valid Region cannot be retrieved.
/// </summary>
public class ErrorItemRegionDtoMapper
{
    /// <summary>
    /// Creates an <see cref="ItemRegionDto"/> with default values in case of an error.
    /// </summary>
    /// <returns>An <see cref="ItemRegionDto"/> with empty or placeholder values, indicating an error region.</returns>
    public static ItemRegionDto ErrorItemRegionDtoMapping() => 
        new (
            Id: Guid.Empty,
            Name: string.Empty,
            Description: string.Empty,
            AreaKm2: double.MinValue,
            IsActive: false,
            State: string.Empty,
            Version: Guid.Empty
        );
}
