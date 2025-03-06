// source
using server.src.Domain.Geography.Administrative.States.Dtos;
using server.src.Domain.Geography.Administrative.States.Models;

namespace server.src.Application.Geography.Administrative.States.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="State"/> model  
/// into an <see cref="ItemStateDto"/> for detailed item representation.
/// </summary>
public static class ItemStateDtoMapper
{
    /// <summary>
    /// Maps a <see cref="State"/> model to an <see cref="ItemStateDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="State"/> model to be mapped.</param>
    /// <returns>An <see cref="ItemStateDto"/> representing the country with full details.</returns>
    public static ItemStateDto ItemStateDtoMapping(
        this State model
    ) => new(
        Id: model.Id,
        Name: model.Name,
        Description: model.Description,
        Capital: model.Capital,
        Population: model.Population,
        AreaKm2: model.AreaKm2,
        Code: model.Code,
        IsActive: model.IsActive,
        Country: model.Country.Name,
        Version: model.Version
    );
}
