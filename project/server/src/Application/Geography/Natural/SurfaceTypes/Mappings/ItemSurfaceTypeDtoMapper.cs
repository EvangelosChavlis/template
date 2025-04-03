// source
using server.src.Domain.Geography.Natural.SurfaceTypes.Dtos;
using server.src.Domain.Geography.Natural.SurfaceTypes.Models;

namespace server.src.Application.Geography.Natural.SurfaceTypes.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="SurfaceType"/> model  
/// into an <see cref="ItemSurfaceTypeDto"/> for detailed item representation.
/// </summary>
public static class ItemSurfaceTypeDtoMapper
{
    /// <summary>
    /// Maps a SurfaceType model to an ItemSurfaceTypeDto.
    /// </summary>
    /// <param name="model">The SurfaceType model that will be mapped to an ItemSurfaceTypeDto.</param>
    /// <returns>An ItemSurfaceTypeDto representing the SurfaceType model with full details for an individual item view.</returns>
    public static ItemSurfaceTypeDto ItemSurfaceTypeDtoMapping(
        this SurfaceType model) => new(
            Id: model.Id,
            Name: model.Name,
            Description: model.Description,
            Code: model.Code,
            IsActive: model.IsActive,
            Version: model.Version
        );
}