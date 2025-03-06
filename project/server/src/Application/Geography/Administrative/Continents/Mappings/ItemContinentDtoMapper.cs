// source
using server.src.Domain.Geography.Administrative.Continents.Dtos;
using server.src.Domain.Geography.Administrative.Continents.Models;

namespace server.src.Application.Geography.Administrative.Continents.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="Continent"/> model  
/// into an <see cref="ItemContinentDto"/> for detailed item representation.
/// </summary>
public static class ItemContinentDtoMapper
{
    /// <summary>
    /// Maps a Continent model to an ItemContinentDto.
    /// </summary>
    /// <param name="model">The Continent model that will be mapped to an ItemContinentDto.</param>
    /// <returns>An ItemContinentDto representing the Continent model with full details for an individual item view.</returns>
    public static ItemContinentDto ItemContinentDtoMapping(
        this Continent model) => new(
            Id: model.Id,
            Name: model.Name,
            Code: model.Code,
            Description: model.Description,
            IsActive: model.IsActive,
            Version: model.Version
        );
}