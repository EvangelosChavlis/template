// source
using server.src.Domain.Geography.Administrative.Countries.Dtos;

namespace server.src.Application.Geography.Administrative.Countries.Mappings;

/// <summary>
/// Provides a method to generate an <see cref="ItemCountryDto"/> with default values  
/// to represent an error state when a valid country cannot be retrieved.
/// </summary>
public class ErrorItemCountryDtoMapper
{
    /// <summary>
    /// Creates an <see cref="ItemCountryDto"/> with default values in case of an error.
    /// </summary>
    /// <returns>An <see cref="ItemCountryDto"/> with empty or placeholder values, indicating an error state.</returns>
    public static ItemCountryDto ErrorItemCountryDtoMapping() => 
        new (
            Id: Guid.Empty,
            Name: string.Empty,
            Description: string.Empty,
            IsoCode: string.Empty,
            Capital: string.Empty,
            Population: int.MinValue,
            AreaKm2: double.MinValue,
            IsActive: false,
            Continent: string.Empty,
            Version: Guid.Empty
        );
}
