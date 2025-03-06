// source
using server.src.Domain.Geography.Administrative.States.Dtos;

namespace server.src.Application.Geography.Administrative.States.Mappings;

/// <summary>
/// Provides a method to generate an <see cref="ItemStateDto"/> with default values  
/// to represent an error state when a valid State cannot be retrieved.
/// </summary>
public class ErrorItemStateDtoMapper
{
    /// <summary>
    /// Creates an <see cref="ItemStateDto"/> with default values in case of an error.
    /// </summary>
    /// <returns>An <see cref="ItemStateDto"/> with empty or placeholder values, indicating an error state.</returns>
    public static ItemStateDto ErrorItemStateDtoMapping() => 
        new (
            Id: Guid.Empty,
            Name: string.Empty,
            Description: string.Empty,
            Capital: string.Empty,
            Population: int.MinValue,
            AreaKm2: double.MinValue,
            Code: string.Empty,
            IsActive: false,
            Country: string.Empty,
            Version: Guid.Empty
        );
}
