// source
using server.src.Domain.Geography.Administrative.Countries.Dtos;
using server.src.Domain.Geography.Administrative.Countries.Models;

namespace server.src.Application.Geography.Administrative.Countries.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="Country"/> model  
/// into an <see cref="ItemCountryDto"/> for detailed item representation.
/// </summary>
public static class ItemCountryDtoMapper
{
    /// <summary>
    /// Maps a <see cref="Country"/> model to an <see cref="ItemCountryDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="Country"/> model to be mapped.</param>
    /// <returns>An <see cref="ItemCountryDto"/> representing the country with full details.</returns>
    public static ItemCountryDto ItemCountryDtoMapping(
        this Country model
    ) => new(
        Id: model.Id,
        Name: model.Name,
        Description: model.Description,
        IsoCode: model.IsoCode,
        Capital: model.Capital,
        Population: model.Population,
        AreaKm2: model.AreaKm2,
        IsActive: model.IsActive,
        Continent: model.Continent.Name,
        Version: model.Version
    );
}
