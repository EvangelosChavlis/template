// source
using server.src.Domain.Geography.Natural.SurfaceTypes.Dtos;
using server.src.Domain.Geography.Natural.SurfaceTypes.Models;

namespace server.src.Application.Geography.Natural.SurfaceTypes.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="SurfaceType"/> model 
/// into a <see cref="ListItemSurfaceTypeDto"/>.
/// This utility class is used to transform SurfaceType data for list views with key details.
/// </summary>
public static class ListItemSurfaceTypeDtoMapper
{
    /// <summary>
    /// Maps a <see cref="SurfaceType"/> model to a <see cref="ListItemSurfaceTypeDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="SurfaceType"/> model that will be mapped.</param>
    /// <returns>A <see cref="ListItemSurfaceTypeDto"/> representing the SurfaceType model with essential details.</returns>
    public static ListItemSurfaceTypeDto ListItemSurfaceTypeDtoMapping(
        this SurfaceType model) => new(
            Id: model.Id,
            Name: model.Name,
            Code: model.Code,
            IsActive: model.IsActive,
            Count: model.Locations.Count
        );
}