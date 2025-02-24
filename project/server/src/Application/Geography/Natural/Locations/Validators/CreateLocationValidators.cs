// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.Locations.Dtos;

namespace server.src.Application.Geography.Natural.Locations.Validators;

public static class CreateLocationValidators
{
    public static ValidationResult Validate(this CreateLocationDto dto)
    {
        var errors = new List<string>();

        // Validation for Longitude
        if (dto.Longitude < -180 || dto.Longitude > 180)
            errors.Add("Longitude must be between -180 and 180 degrees.");

        // Validation for Latitude
        if (dto.Latitude < -90 || dto.Latitude > 90)
            errors.Add("Latitude must be between -90 and 90 degrees.");

        // Validation for Altitude (assuming no specific limit, but you can set one)
        if (dto.Altitude < -500 || dto.Altitude > 9000) 
            errors.Add("Altitude must be a realistic value between -500 and 9000 meters.");

        // Validation for required foreign keys
        if (dto.TimezoneId == Guid.Empty)
            errors.Add("TimezoneId is required.");

        if (dto.TerrainTypeId == Guid.Empty)
            errors.Add("TerrainTypeId is required.");

        if (dto.ClimateZoneId == Guid.Empty)
            errors.Add("ClimateZoneId is required.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}