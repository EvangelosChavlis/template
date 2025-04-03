// source
using server.src.Domain.Geography.Natural.NaturalFeatures.Dtos;

namespace server.src.Application.Geography.Natural.NaturalFeatures.Mappings;

/// <summary>
/// Provides a method to generate an <see cref="ItemNaturalFeatureDto"/> with default values  
/// to represent an error state when a valid NaturalFeature cannot be retrieved.
/// </summary>
public class ErrorItemNaturalFeatureDtoMapper
{
    /// <summary>
    /// Creates an <see cref="ItemNaturalFeatureDto"/> with default values in case of an error.
    /// </summary>
    /// <returns>An <see cref="ItemNaturalFeatureDto"/> with empty or placeholder values, indicating an error state.</returns>
    public static ItemNaturalFeatureDto ErrorItemNaturalFeatureDtoMapping() => 
        new (
            Id: Guid.Empty,
            Name: string.Empty,
            Description: string.Empty,
            Code: string.Empty,
            IsActive: false,
            Version: Guid.Empty 
        );
}