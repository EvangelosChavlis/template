namespace server.src.Domain.Geography.Natural.NaturalFeatures.Dtos;

/// <summary>
/// DTO for updating an existing natural feature.
/// </summary>
/// <param name="Name">The updated name of the natural feature.</param>
/// <param name="Description">The updated description of the natural feature.</param>
/// <param name="Code">A brief code of the natural feature.</param>
/// <param name="Version">The version identifier for concurrency control.</param>
public record UpdateNaturalFeatureDto(
    string Name,
    string Description,
    string Code,
    Guid Version
);