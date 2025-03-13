// source
using server.src.Domain.Geography.Administrative.Regions.Dtos;

namespace server.src.Application.Geography.Administrative.States.Mappings;

/// <summary>
/// Provides mapping functionality to convert an <see cref="ImportRegionDto"/> into a <see cref="CreateRegionDto"/>.
/// This utility class is used to transform imported region data into a format suitable for creating a new region entity.
/// </summary>
public static class ImportRegionModelMapper
{
    /// <summary>
    /// Maps an <see cref="ImportRegionDto"/> to a <see cref="CreateRegionDto"/>, 
    /// preparing it for region entity creation by adding the associated state ID.
    /// </summary>
    /// <param name="dto">The data transfer object containing imported region details.</param>
    /// <param name="stateId">The unique identifier of the state the region belongs to.</param>
    /// <returns>A new <see cref="CreateRegionDto"/> instance populated with data from the provided DTO.</returns>
    public static CreateRegionDto ImportRegionMapping(
        this ImportRegionDto dto, 
        Guid stateId
    ) => new (
        Name: dto.Name,
        Description: dto.Description,
        AreaKm2: dto.AreaKm2,
        Code: dto.Code,
        StateId: stateId
    );
}