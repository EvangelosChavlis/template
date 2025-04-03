namespace server.src.Domain.Geography.Natural.NaturalFeatures.Dtos;

/// <summary>
/// DTO for selecting a natural feature.
/// </summary>
/// <param name="Id">The unique identifier of the natural feature.</param>
/// <param name="Name">The name of the natural feature.</param>
public record PickerNaturalFeatureDto(
    Guid Id,
    string Name
);