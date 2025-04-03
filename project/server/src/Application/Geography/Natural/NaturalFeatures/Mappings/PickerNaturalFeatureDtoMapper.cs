// source
using server.src.Domain.Geography.Natural.NaturalFeatures.Dtos;
using server.src.Domain.Geography.Natural.NaturalFeatures.Models;

namespace server.src.Application.Geography.Natural.NaturalFeatures.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="NaturalFeature"/> model 
/// into a <see cref="PickerNaturalFeatureDto"/>.
/// This mapper is used to transform NaturalFeature data for selection lists or dropdowns.
/// </summary>
public static class PickerNaturalFeatureDtoMapper
{
    /// <summary>
    /// Maps a <see cref="NaturalFeature"/> model to a <see cref="PickerNaturalFeatureDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="NaturalFeature"/> model that will be mapped.</param>
    /// <returns>A <see cref="PickerNaturalFeatureDto"/> containing essential details for selection purposes.</returns>
    public static PickerNaturalFeatureDto PickerNaturalFeatureDtoMapping(
        this NaturalFeature model) => new(
            Id: model.Id,
            Name: model.Name
        );
}
