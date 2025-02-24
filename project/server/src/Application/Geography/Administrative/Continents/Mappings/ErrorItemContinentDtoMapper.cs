// source
using server.src.Domain.Geography.Administrative.Continents.Dtos;

namespace server.src.Application.Geography.Administrative.Continents.Mappings;

/// <summary>
/// Provides a method to generate an <see cref="ItemContinentDto"/> with default values  
/// to represent an error state when a valid continent cannot be retrieved.
/// </summary>
public class ErrorItemContinentDtoMapper
{
    /// <summary>
    /// Creates an <see cref="ItemContinentDto"/> with default values in case of an error.
    /// </summary>
    /// <returns>An <see cref="ItemContinentDto"/> with empty or placeholder values, indicating an error state.</returns>
    public static ItemContinentDto ErrorItemContinentDtoMapping() => 
        new (
            Id: Guid.Empty,
            Name: string.Empty,
            Description: string.Empty,
            IsActive: false,
            Version: Guid.Empty 
        );
}