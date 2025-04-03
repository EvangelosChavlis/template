using server.src.Domain.Weather.Collections.Warnings.Dtos;

namespace server.src.Application.Weather.Collections.Warnings.Mappings;

/// <summary>
/// Provides a method to generate an <see cref="ItemWarningDto"/> with default values  
/// to represent an error state when a valid warning cannot be retrieved.
/// </summary>
public class ErrorItemWarningDtoMapper
{
    /// <summary>
    /// Creates an <see cref="ItemWarningDto"/> with default values in case of an error.
    /// </summary>
    /// <returns>An <see cref="ItemWarningDto"/> with empty or placeholder values, indicating an error state.</returns>
    public static ItemWarningDto ErrorItemWarningDtoMapping() => 
        new (
            Id: Guid.Empty,
            Name: string.Empty,
            Description: string.Empty,
            Code: string.Empty,
            RecommendedActions: string.Empty,
            IsActive: false,
            Version: Guid.Empty 
        );
}