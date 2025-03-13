// source
using server.src.Domain.Geography.Administrative.States.Dtos;

namespace server.src.Application.Geography.Administrative.States.Mappings;

/// <summary>
/// Provides mapping functionality to convert an <see cref="ImportStateDto"/> into a <see cref="CreateStateDto"/>.
/// This utility class is used to transform imported state data into a format suitable for creating a new state entity.
/// </summary>
public static class ImportStateModelMapper
{
    /// <summary>
    /// Maps an <see cref="ImportStateDto"/> to a <see cref="CreateStateDto"/>, 
    /// preparing it for state entity creation by adding the associated country ID.
    /// </summary>
    /// <param name="dto">The data transfer object containing imported state details.</param>
    /// <param name="countryId">The unique identifier of the country the state belongs to.</param>
    /// <returns>A new <see cref="CreateStateDto"/> instance populated with data from the provided DTO.</returns>
    public static CreateStateDto ImportStateMapping(
        this ImportStateDto dto, 
        Guid countryId
    ) => new (
        Name: dto.Name,
        Description: dto.Description,
        Capital: dto.Capital,
        Population: dto.Population,
        AreaKm2: dto.AreaKm2,
        Code: dto.Code,
        CountryId: countryId
    );
}