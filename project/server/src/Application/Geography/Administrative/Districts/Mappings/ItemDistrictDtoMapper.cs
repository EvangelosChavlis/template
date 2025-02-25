// source
using server.src.Domain.Geography.Administrative.Districts.Dtos;
using server.src.Domain.Geography.Administrative.Districts.Models;

namespace server.src.Application.Geography.Administrative.Districts.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="District"/> model  
/// into an <see cref="ItemDistrictDto"/> for detailed item representation.
/// </summary>
public static class ItemDistrictDtoMapper
{
    /// <summary>
    /// Maps a <see cref="District"/> model to an <see cref="ItemDistrictDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="District"/> model to be mapped.</param>
    /// <returns>An <see cref="ItemDistrictDto"/> representing the country with full details.</returns>
    public static ItemDistrictDto ItemDistrictDtoMapping(
        this District model
    ) => new(
        Id: model.Id,
        Name: model.Name,
        Description: model.Description,
        Population: model.Population,
        IsActive: model.IsActive,
        Municipality: model.Municipality.Name,
        Version: model.Version
    );
}
