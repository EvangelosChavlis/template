// source
using server.src.Domain.Geography.Administrative.Continents.Dtos;
using server.src.Domain.Geography.Administrative.Continents.Models;

namespace server.src.Application.Geography.Administrative.Continents.Mappings;

/// <summary>
/// Provides mapping functionality to update an existing <see cref="Continent"/> model 
/// using data from an <see cref="UpdateContinentDto"/>.
/// This utility class ensures that the continent entity is updated efficiently with new details.
/// </summary>
public static class UpdateContinentMapper
{
    /// <summary>
    /// Updates an existing <see cref="Continent"/> model with data from an <see cref="UpdateContinentDto"/>.
    /// </summary>
    /// <param name="dto">The data transfer object containing updated continent details.</param>
    /// <param name="model">The existing <see cref="Continent"/> model to be updated.</param>
    public static void UpdateContinentMapping(this UpdateContinentDto dto, Continent model)
    {
        model.Name = dto.Name;
        model.Code = dto.Code;
        model.IsActive = model.IsActive;
        model.Description = dto.Description;
        model.Version = Guid.NewGuid();
    }
}
