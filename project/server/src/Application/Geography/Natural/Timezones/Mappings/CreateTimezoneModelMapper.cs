// source
using server.src.Domain.Geography.Natural.Timezones.Dtos;
using server.src.Domain.Geography.Natural.Timezones.Models;

namespace server.src.Application.Geography.Natural.Timezones.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="CreateTimezoneDto"/> into a <see cref="Timezone"/> model.
/// This utility class is used to create new terrain type instances based on provided data transfer objects.
/// </summary>
public static class CreateTimezoneModelMapper
{
    /// <summary>
    /// Maps a <see cref="CreateTimezoneDto"/> to a <see cref="Timezone"/> model, creating a new timezone entity.
    /// </summary>
    /// <param name="dto">The data transfer object containing timezone details.</param>
    /// <returns>A newly created <see cref="Timezone"/> model populated with data from the provided DTO.</returns>
    public static Timezone CreateTimezoneModelMapping(this CreateTimezoneDto dto)
        => new()
        {
            Name = dto.Name,
            UtcOffset = dto.UtcOffset,
            SupportsDaylightSaving = dto.SupportsDaylightSaving,
            Description = dto.Description,
            IsActive = true,
            Version = Guid.NewGuid()
        };
}
