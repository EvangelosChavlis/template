namespace server.src.Domain.Geography.Natural.NaturalFeatures.Dtos;

/// <summary>
/// DTO representing a natural feature item.
/// </summary>
/// <param name="Id">The unique identifier of the natural feature.</param>
/// <param name="Name">The name of the natural feature.</param>
/// <param name="Description">A brief description of the natural feature.</param>
/// <param name="Code">A unique code of the natural feature.</param>
/// <param name="IsActive">Indicates whether the natural feature is active.</param>
/// <param name="Version">The version identifier for concurrency control.</param>
public record ItemNaturalFeatureDto(
    Guid Id,
    string Name,
    string Description,
    string Code,
    bool IsActive,
    Guid Version
);