// source
using server.src.Domain.Geography.Natural.NaturalFeatures.Dtos;
using server.src.Domain.Geography.Natural.NaturalFeatures.Models;

namespace server.src.Application.Geography.Natural.NaturalFeatures.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="NaturalFeature"/> model 
/// into a <see cref="ListItemNaturalFeatureDto"/>.
/// This utility class is used to transform NaturalFeature data for list views with key details.
/// </summary>
public static class ListItemNaturalFeatureDtoMapper
{
    /// <summary>
    /// Maps a <see cref="NaturalFeature"/> model to a <see cref="ListItemNaturalFeatureDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="NaturalFeature"/> model that will be mapped.</param>
    /// <returns>A <see cref="ListItemNaturalFeatureDto"/> representing the NaturalFeature model with essential details.</returns>
    public static ListItemNaturalFeatureDto ListItemNaturalFeatureDtoMapping(
        this NaturalFeature model) => new(
            Id: model.Id,
            Name: model.Name,
            Code: model.Code,
            IsActive: model.IsActive,
            Count: model.Locations.Count
        );
}