// source
using server.src.Domain.Geography.Administrative.Municipalities.Dtos;
using server.src.Domain.Geography.Administrative.Municipalities.Models;

namespace server.src.Application.Geography.Administrative.Municipalities.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="Municipality"/> model  
/// into an <see cref="ItemMunicipalityDto"/> for detailed item representation.
/// </summary>
public static class ItemMunicipalityDtoMapper
{
    /// <summary>
    /// Maps a <see cref="Municipality"/> model to an <see cref="ItemMunicipalityDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="Municipality"/> model to be mapped.</param>
    /// <returns>An <see cref="ItemMunicipalityDto"/> representing the country with full details.</returns>
    public static ItemMunicipalityDto ItemMunicipalityDtoMapping(
        this Municipality model
    ) => new(
        Id: model.Id,
        Name: model.Name,
        Description: model.Description,
        Population: model.Population,
        IsActive: model.IsActive,
        Region: model.Region.Name,
        Version: model.Version
    );
}
