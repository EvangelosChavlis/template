// source
using server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;

namespace server.src.Application.Geography.Natural.Neighborhoods.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="Neighborhood"/> model 
/// into a <see cref="ListItemNeighborhoodDto"/>.
/// This utility class is used to transform country data for list views with key details.
/// </summary>
public static class ListItemNeighborhoodDtoMapper
{
    /// <summary>
    /// Maps a <see cref="Neighborhood"/> model to a <see cref="ListItemNeighborhoodDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="Neighborhood"/> model that will be mapped.</param>
    /// <returns>A <see cref="ListItemNeighborhoodDto"/> representing the country model with essential details.</returns>
    public static ListItemNeighborhoodDto ListItemNeighborhoodDtoMapping(
        this Neighborhood model) => new(
            Id: model.Id,
            Name: model.Name,
            Zipcode: model.Zipcode,
            Code: model.Code,
            IsActive: model.IsActive
        );
}
