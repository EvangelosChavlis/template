// source
using server.src.Domain.Geography.Administrative.Stations.Dtos;
using server.src.Domain.Geography.Administrative.Stations.Models;
using server.src.Domain.Geography.Natural.Locations.Models;
using server.src.Domain.Geography.Natural.Locations.Extensions;

namespace server.src.Application.Geography.Administrative.Stations.Mappings;

/// <summary>
/// Provides mapping functionality to convert a <see cref="Station"/> model  
/// into an <see cref="ItemStationDto"/> for detailed item representation.
/// </summary>
public static class ItemStationDtoMapper
{
    /// <summary>
    /// Maps a <see cref="Station"/> model to an <see cref="ItemStationDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="Station"/> model to be mapped.</param>
    /// <returns>An <see cref="ItemStationDto"/> representing the country with full details.</returns>
    public static ItemStationDto ItemStationDtoMapping(
        this Station model
    ) => new(
        Id: model.Id,
        Name: model.Name,
        Description: model.Description,
        Code: model.Code,
        IsActive: model.IsActive,
        Location: model.Location.GetCoordinates(),
        Version: model.Version
    );
}
