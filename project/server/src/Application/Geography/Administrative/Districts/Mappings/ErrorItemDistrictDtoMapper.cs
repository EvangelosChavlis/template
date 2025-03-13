// source
using server.src.Domain.Geography.Administrative.Districts.Dtos;

namespace server.src.Application.Geography.Administrative.Districts.Mappings;

/// <summary>
/// Provides a method to generate an <see cref="ItemDistrictDto"/> with default values  
/// to represent an error district when a valid District cannot be retrieved.
/// </summary>
public class ErrorItemDistrictDtoMapper
{
    /// <summary>
    /// Creates an <see cref="ItemDistrictDto"/> with default values in case of an error.
    /// </summary>
    /// <returns>An <see cref="ItemDistrictDto"/> with empty or placeholder values, indicating an error district.</returns>
    public static ItemDistrictDto ErrorItemDistrictDtoMapping() => 
        new (
            Id: Guid.Empty,
            Name: string.Empty,
            Description: string.Empty,
            Population: long.MinValue,
            AreaKm2: double.MinValue,
            Code: string.Empty,
            IsActive: false,
            Municipality: string.Empty,
            Version: Guid.Empty
        );
}
