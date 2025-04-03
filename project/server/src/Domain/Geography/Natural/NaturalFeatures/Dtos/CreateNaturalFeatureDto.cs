namespace server.src.Domain.Geography.Natural.NaturalFeatures.Dtos;

/// <summary>
/// DTO for creating a new natural feature.
/// </summary>
/// <param name="Name">The name of the natural feature.</param>
/// <param name="Description">A brief description of the natural feature.</param>
/// <param name="Code">A brief code of the natural feature.</param>
public record CreateNaturalFeatureDto(
    string Name,
    string Description,
    string Code
);