// source
using server.src.Domain.Geography.Administrative.Countries.Dtos;
using server.src.Domain.Geography.Administrative.Countries.Models;

namespace server.src.Application.Geography.Natural.Countries.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="Country"/> model 
/// into a <see cref="ListItemCountryDto"/>.
/// This utility class is used to transform country data for list views with key details.
/// </summary>
public static class ListItemCountryDtoMapper
{
    /// <summary>
    /// Maps a <see cref="Country"/> model to a <see cref="ListItemCountryDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="Country"/> model that will be mapped.</param>
    /// <returns>A <see cref="ListItemCountryDto"/> representing the country model with essential details.</returns>
    public static ListItemCountryDto ListItemCountryDtoMapping(
        this Country model) => new(
            Id: model.Id,
            Name: model.Name,
            IsoCode: model.IsoCode,
            IsActive: model.IsActive,
            Population: model.Population,
            Count: model.States.Count
        );
}
