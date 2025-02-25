// source
using server.src.Domain.Geography.Administrative.Districts.Dtos;
using server.src.Domain.Geography.Administrative.Districts.Models;

namespace server.src.Application.Geography.Natural.Districts.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="District"/> model 
/// into a <see cref="ListItemDistrictDto"/>.
/// This utility class is used to transform country data for list views with key details.
/// </summary>
public static class ListItemDistrictDtoMapper
{
    /// <summary>
    /// Maps a <see cref="District"/> model to a <see cref="ListItemDistrictDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="District"/> model that will be mapped.</param>
    /// <returns>A <see cref="ListItemDistrictDto"/> representing the country model with essential details.</returns>
    public static ListItemDistrictDto ListItemDistrictDtoMapping(
        this District model) => new(
            Id: model.Id,
            Name: model.Name,
            Population: model.Population,
            IsActive: model.IsActive,
            Count: model.Neighborhoods.Count
        );
}
