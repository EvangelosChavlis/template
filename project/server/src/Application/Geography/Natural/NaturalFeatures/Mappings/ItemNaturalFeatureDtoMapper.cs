// source
using server.src.Domain.Geography.Natural.NaturalFeatures.Dtos;
using server.src.Domain.Geography.Natural.NaturalFeatures.Models;

namespace server.src.Application.Geography.Natural.NaturalFeatures.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="NaturalFeature"/> model  
/// into an <see cref="ItemNaturalFeatureDto"/> for detailed item representation.
/// </summary>
public static class ItemNaturalFeatureDtoMapper
{
    /// <summary>
    /// Maps a NaturalFeature model to an ItemNaturalFeatureDto.
    /// </summary>
    /// <param name="model">The NaturalFeature model that will be mapped to an ItemNaturalFeatureDto.</param>
    /// <returns>An ItemNaturalFeatureDto representing the NaturalFeature model with full details for an individual item view.</returns>
    public static ItemNaturalFeatureDto ItemNaturalFeatureDtoMapping(
        this NaturalFeature model) => new(
            Id: model.Id,
            Name: model.Name,
            Description: model.Description,
            Code: model.Code,
            IsActive: model.IsActive,
            Version: model.Version
        );
}