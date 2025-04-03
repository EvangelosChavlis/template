// source
using server.src.Domain.Geography.Natural.SurfaceTypes.Dtos;
using server.src.Domain.Geography.Natural.SurfaceTypes.Models;

namespace server.src.Application.Geography.Natural.SurfaceTypes.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="SurfaceType"/> model 
/// into a <see cref="PickerSurfaceTypeDto"/>.
/// This mapper is used to transform SurfaceType data for selection lists or dropdowns.
/// </summary>
public static class PickerSurfaceTypeDtoMapper
{
    /// <summary>
    /// Maps a <see cref="SurfaceType"/> model to a <see cref="PickerSurfaceTypeDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="SurfaceType"/> model that will be mapped.</param>
    /// <returns>A <see cref="PickerSurfaceTypeDto"/> containing essential details for selection purposes.</returns>
    public static PickerSurfaceTypeDto PickerSurfaceTypeDtoMapping(
        this SurfaceType model) => new(
            Id: model.Id,
            Name: model.Name
        );
}
