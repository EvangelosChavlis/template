// source
using server.src.Domain.Geography.Natural.NaturalFeatures.Dtos;
using server.src.Domain.Geography.Natural.NaturalFeatures.Models;

namespace server.src.Application.Geography.Natural.NaturalFeatures.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="CreateNaturalFeatureDto"/> into a <see cref="NaturalFeature"/> model.
/// This utility class is used to create new natural feature instances based on provided data transfer objects.
/// </summary>
public static class CreateNaturalFeatureModelMapper
{
    /// <summary>
    /// Maps a <see cref="CreateNaturalFeatureDto"/> to a <see cref="NaturalFeature"/> model, creating a new NaturalFeature entity.
    /// </summary>
    /// <param name="dto">The data transfer object containing NaturalFeature details.</param>
    /// <returns>A newly created <see cref="NaturalFeature"/> model populated with data from the provided DTO.</returns>
    public static NaturalFeature CreateNaturalFeatureModelMapping(this CreateNaturalFeatureDto dto)
        => new()
        {
            Name = dto.Name,
            Description = dto.Description,
            Code = dto.Code,
            IsActive = true,
            Version = Guid.NewGuid()
        };
}
