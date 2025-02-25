// source
using server.src.Domain.Geography.Administrative.Regions.Dtos;
using server.src.Domain.Geography.Administrative.Regions.Models;

namespace server.src.Application.Geography.Administrative.Regions.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="Region"/> model  
/// into an <see cref="ItemRegionDto"/> for detailed item representation.
/// </summary>
public static class ItemRegionDtoMapper
{
    /// <summary>
    /// Maps a <see cref="Region"/> model to an <see cref="ItemRegionDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="Region"/> model to be mapped.</param>
    /// <returns>An <see cref="ItemRegionDto"/> representing the country with full details.</returns>
    public static ItemRegionDto ItemRegionDtoMapping(
        this Region model
    ) => new(
        Id: model.Id,
        Name: model.Name,
        Description: model.Description,
        AreaKm2: model.AreaKm2,
        IsActive: model.IsActive,
        State: model.State.Name,
        Version: model.Version
    );
}
