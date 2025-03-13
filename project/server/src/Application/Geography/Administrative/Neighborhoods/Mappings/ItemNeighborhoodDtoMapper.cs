// source
using server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;

namespace server.src.Application.Geography.Administrative.Neighborhoods.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="Neighborhood"/> model  
/// into an <see cref="ItemNeighborhoodDto"/> for detailed item representation.
/// </summary>
public static class ItemNeighborhoodDtoMapper
{
    /// <summary>
    /// Maps a <see cref="Neighborhood"/> model to an <see cref="ItemNeighborhoodDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="Neighborhood"/> model to be mapped.</param>
    /// <returns>An <see cref="ItemNeighborhoodDto"/> representing the country with full details.</returns>
    public static ItemNeighborhoodDto ItemNeighborhoodDtoMapping(
        this Neighborhood model
    ) => new(
        Id: model.Id,
        Name: model.Name,
        Description: model.Description,
        Population: model.Population,
        AreaKm2: model.AreaKm2,
        Zipcode: model.Zipcode,
        Code: model.Code,
        IsActive: model.IsActive,
        District: model.District.Name,
        Version: model.Version
    );
}
