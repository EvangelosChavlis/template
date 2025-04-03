// source
using server.src.Domain.Geography.Natural.NaturalFeatures.Dtos;
using server.src.Domain.Geography.Natural.NaturalFeatures.Models;

namespace server.src.Application.Geography.Natural.NaturalFeatures.Mappings;

/// <summary>
/// Provides mapping functionality to update an existing <see cref="NaturalFeature"/> model 
/// using data from an <see cref="UpdateNaturalFeatureDto"/>.
/// This utility class ensures that the NaturalFeature entity is updated efficiently with new details.
/// </summary>
public static class UpdateNaturalFeatureMapper
{
    /// <summary>
    /// Updates an existing <see cref="NaturalFeature"/> model with data from an <see cref="UpdateNaturalFeatureDto"/>.
    /// </summary>
    /// <param name="dto">The data transfer object containing updated NaturalFeature details.</param>
    /// <param name="model">The existing <see cref="NaturalFeature"/> model to be updated.</param>
    public static void UpdateNaturalFeatureMapping(this UpdateNaturalFeatureDto dto, NaturalFeature model)
    {
        model.Name = dto.Name;
        model.Description = dto.Description;
        model.Code = dto.Code;
        model.IsActive = model.IsActive;
        model.Version = Guid.NewGuid();
    }
}
