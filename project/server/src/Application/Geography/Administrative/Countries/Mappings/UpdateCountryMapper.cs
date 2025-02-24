// source
using server.src.Domain.Geography.Administrative.Continents.Models;
using server.src.Domain.Geography.Administrative.Countries.Dtos;
using server.src.Domain.Geography.Administrative.Countries.Models;

namespace server.src.Application.Geography.Administrative.Countries.Mappings;

/// <summary>
/// Provides mapping functionality to update an existing <see cref="Country"/> model 
/// using data from an <see cref="UpdateCountryDto"/>.
/// This utility class ensures that the country entity is updated efficiently with new details.
/// </summary>
public static class UpdateCountryMapper
{
    /// <summary>
    /// Updates an existing <see cref="Country"/> model with data from an <see cref="UpdateCountryDto"/>.
    /// </summary>
    /// <param name="dto">The data transfer object containing updated country details.</param>
    /// <param name="modelCountry">The existing <see cref="Country"/> model to be updated.</param>
    public static void UpdateCountryMapping(
        this UpdateCountryDto dto, 
        Country modelCountry,
        Continent modelContinent
    )
    {
        // Mapping all properties
        modelCountry.Name = dto.Name;
        modelCountry.Description = dto.Description;
        modelCountry.IsoCode = dto.IsoCode;
        modelCountry.Capital = dto.Capital;
        modelCountry.Population = dto.Population;
        modelCountry.AreaKm2 = dto.AreaKm2;
        modelCountry.IsActive = modelCountry.IsActive;
        modelCountry.Continent = modelContinent;
        modelCountry.ContinentId = modelContinent.Id;
        modelCountry.Version = dto.Version;
    }
}