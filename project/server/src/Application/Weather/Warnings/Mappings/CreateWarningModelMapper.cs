// source
using server.src.Domain.Weather.Warnings.Dtos;
using server.src.Domain.Weather.Warnings.Models;

namespace server.src.Application.Weather.Warnings.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="CreateWarningDto"/> into a <see cref="Warning"/> model.
/// This utility class is used to create new warning instances based on provided data transfer objects.
/// </summary>
public static class CreateWarningModelMapper
{
    /// <summary>
    /// Maps a <see cref="CreateWarningDto"/> to a <see cref="Warning"/> model, creating a new warning entity.
    /// </summary>
    /// <param name="dto">The data transfer object containing warning details.</param>
    /// <returns>A newly created <see cref="Warning"/> model populated with data from the provided DTO.</returns>
    public static Warning CreateWarningModelMapping(this CreateWarningDto dto)
        => new()
        {
            Name = dto.Name,
            Description = dto.Description,
            RecommendedActions = dto.RecommendedActions,
            IsActive = true,
            Version = Guid.NewGuid()
        };
}
