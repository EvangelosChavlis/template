// source
using server.src.Domain.Geography.Administrative.Municipalities.Dtos;

namespace server.src.Application.Geography.Administrative.Municipalities.Mappings;

/// <summary>
/// Provides a method to generate an <see cref="ItemMunicipalityDto"/> with default values  
/// to represent an error municipality when a valid Municipality cannot be retrieved.
/// </summary>
public class ErrorItemMunicipalityDtoMapper
{
    /// <summary>
    /// Creates an <see cref="ItemMunicipalityDto"/> with default values in case of an error.
    /// </summary>
    /// <returns>An <see cref="ItemMunicipalityDto"/> with empty or placeholder values, indicating an error municipality.</returns>
    public static ItemMunicipalityDto ErrorItemMunicipalityDtoMapping() => 
        new (
            Id: Guid.Empty,
            Name: string.Empty,
            Description: string.Empty,
            Population: long.MinValue,
            IsActive: false,
            Region: string.Empty,
            Version: Guid.Empty
        );
}
