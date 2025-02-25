// source
using server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;

namespace server.src.Application.Geography.Administrative.Neighborhoods.Mappings;

/// <summary>
/// Provides a method to generate an <see cref="ItemNeighborhoodDto"/> with default values  
/// to represent an error Neighborhood when a valid Neighborhood cannot be retrieved.
/// </summary>
public class ErrorItemNeighborhoodDtoMapper
{
    /// <summary>
    /// Creates an <see cref="ItemNeighborhoodDto"/> with default values in case of an error.
    /// </summary>
    /// <returns>An <see cref="ItemNeighborhoodDto"/> with empty or placeholder values, indicating an error Neighborhood.</returns>
    public static ItemNeighborhoodDto ErrorItemNeighborhoodDtoMapping() => 
        new (
            Id: Guid.Empty,
            Name: string.Empty,
            Description: string.Empty,
            Population: long.MinValue,
            Zipcode: string.Empty,
            IsActive: false,
            District: string.Empty,
            Version: Guid.Empty
        );
}
