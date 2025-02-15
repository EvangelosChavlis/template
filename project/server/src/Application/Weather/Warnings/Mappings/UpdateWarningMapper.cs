// source
using server.src.Domain.Weather.Warnings.Dtos;
using server.src.Domain.Weather.Warnings.Models;

namespace server.src.Application.Weather.Warnings.Mappings;

/// <summary>
/// Provides mapping functionality to update an existing <see cref="Warning"/> model 
/// using data from an <see cref="UpdateWarningDto"/>.
/// This utility class ensures that the warning entity is updated efficiently with new details.
/// </summary>
public static class UpdateWarningMapper
{
    /// <summary>
    /// Updates an existing <see cref="Warning"/> model with data from an <see cref="UpdateWarningDto"/>.
    /// </summary>
    /// <param name="dto">The data transfer object containing updated warning details.</param>
    /// <param name="model">The existing <see cref="Warning"/> model to be updated.</param>
    public static void UpdateWarningMapping(this UpdateWarningDto dto, Warning model)
    {
        model.Name = dto.Name;
        model.Description = dto.Description;
        model.RecommendedActions = dto.RecommendedActions;
        model.Version = Guid.NewGuid();
    }
}
