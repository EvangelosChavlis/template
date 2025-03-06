// source
using server.src.Domain.Geography.Administrative.States.Dtos;
using server.src.Domain.Geography.Administrative.States.Models;

namespace server.src.Application.Geography.Natural.States.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="State"/> model 
/// into a <see cref="ListItemStateDto"/>.
/// This utility class is used to transform country data for list views with key details.
/// </summary>
public static class ListItemStateDtoMapper
{
    /// <summary>
    /// Maps a <see cref="State"/> model to a <see cref="ListItemStateDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="State"/> model that will be mapped.</param>
    /// <returns>A <see cref="ListItemStateDto"/> representing the country model with essential details.</returns>
    public static ListItemStateDto ListItemStateDtoMapping(
        this State model) => new(
            Id: model.Id,
            Name: model.Name,
            Population: model.Population,
            Code: model.Code,
            IsActive: model.IsActive,
            Count: model.Regions.Count
        );
}
