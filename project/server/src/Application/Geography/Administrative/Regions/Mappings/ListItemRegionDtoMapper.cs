// source
using server.src.Domain.Geography.Administrative.Regions.Dtos;
using server.src.Domain.Geography.Administrative.Regions.Models;

namespace server.src.Application.Geography.Natural.Regions.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="Region"/> model 
/// into a <see cref="ListItemRegionDto"/>.
/// This utility class is used to transform country data for list views with key details.
/// </summary>
public static class ListItemRegionDtoMapper
{
    /// <summary>
    /// Maps a <see cref="Region"/> model to a <see cref="ListItemRegionDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="Region"/> model that will be mapped.</param>
    /// <returns>A <see cref="ListItemRegionDto"/> representing the country model with essential details.</returns>
    public static ListItemRegionDto ListItemRegionDtoMapping(
        this Region model) => new(
            Id: model.Id,
            Name: model.Name,
            AreaKm2: model.AreaKm2,
            Code: model.Code,
            IsActive: model.IsActive,
            Count: model.Municipalities.Count
        );
}
