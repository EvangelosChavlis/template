// source
using server.src.Domain.Geography.Administrative.Countries.Models;
using server.src.Domain.Geography.Administrative.States.Dtos;
using server.src.Domain.Geography.Administrative.States.Models;

namespace server.src.Application.Geography.Administrative.States.Mappings;

/// <summary>
/// Provides mapping functionality to update an existing <see cref="State"/> model 
/// using data from an <see cref="UpdateStateDto"/>.
/// This utility class ensures that the country entity is updated efficiently with new details.
/// </summary>
public static class UpdateStateMapper
{
    /// <summary>
    /// Updates an existing <see cref="State"/> model with data from an <see cref="UpdateStateDto"/>.
    /// </summary>
    /// <param name="dto">The data transfer object containing updated country details.</param>
    /// <param name="modelState">The existing <see cref="State"/> model to be updated.</param>
    public static void UpdateStateMapping(
        this UpdateStateDto dto, 
        State modelState,
        Country modelCountry
    )
    {
        // Mapping all properties
        modelState.Name = dto.Name;
        modelState.Description = dto.Description;
        modelState.Capital = dto.Capital;
        modelState.Population = dto.Population;
        modelState.AreaKm2 = dto.AreaKm2;
        modelState.IsActive = modelState.IsActive;
        modelState.Country = modelCountry;
        modelState.CountryId = modelCountry.Id;
        modelState.Version = Guid.NewGuid();
    }
}