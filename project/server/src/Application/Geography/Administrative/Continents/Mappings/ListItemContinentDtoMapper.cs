// source
using server.src.Domain.Geography.Administrative.Continents.Dtos;
using server.src.Domain.Geography.Administrative.Continents.Models;

namespace server.src.Application.Geography.Natural.Continents.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="Continent"/> model 
/// into a <see cref="ListItemContinentDto"/>.
/// This utility class is used to transform continent data for list views with key details.
/// </summary>
public static class ListItemContinentDtoMapper
{
    /// <summary>
    /// Maps a <see cref="Continent"/> model to a <see cref="ListItemContinentDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="Continent"/> model that will be mapped.</param>
    /// <returns>A <see cref="ListItemContinentDto"/> representing the continent model with essential details.</returns>
    public static ListItemContinentDto ListItemContinentDtoMapping(
        this Continent model) => new(
            Id: model.Id,
            Name: model.Name,
            IsActive: model.IsActive,
            Count: model.Countries.Count
        );
}