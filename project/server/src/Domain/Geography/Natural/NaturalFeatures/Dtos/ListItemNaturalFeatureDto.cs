namespace server.src.Domain.Geography.Natural.NaturalFeatures.Dtos;

/// <summary>
/// DTO representing a natural feature item in a list.
/// </summary>
/// <param name="Id">The unique identifier of the natural feature.</param>
/// <param name="Name">The name of the natural feature.</param>
/// <param name="Code">A unique code of the natural feature.</param>
/// <param name="IsActive">Indicates whether the natural feature is active.</param>
/// <param name="Count">The number of associated items.</param>
public record ListItemNaturalFeatureDto(
    Guid Id,
    string Name,
    string Code,
    bool IsActive,
    int Count
);