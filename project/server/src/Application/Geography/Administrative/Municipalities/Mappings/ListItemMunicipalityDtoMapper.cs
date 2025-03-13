// source
using server.src.Domain.Geography.Administrative.Municipalities.Dtos;
using server.src.Domain.Geography.Administrative.Municipalities.Models;

namespace server.src.Application.Geography.Natural.Municipalities.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="Municipality"/> model 
/// into a <see cref="ListItemMunicipalityDto"/>.
/// This utility class is used to transform country data for list views with key details.
/// </summary>
public static class ListItemMunicipalityDtoMapper
{
    /// <summary>
    /// Maps a <see cref="Municipality"/> model to a <see cref="ListItemMunicipalityDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="Municipality"/> model that will be mapped.</param>
    /// <returns>A <see cref="ListItemMunicipalityDto"/> representing the country model with essential details.</returns>
    public static ListItemMunicipalityDto ListItemMunicipalityDtoMapping(
        this Municipality model) => new(
            Id: model.Id,
            Name: model.Name,
            Population: model.Population,
            Code: model.Code,
            IsActive: model.IsActive,
            Count: model.Districts.Count
        );
}
