// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.Locations.Dtos;

namespace server.src.Application.Geography.Natural.Locations.Validators;

public static class UpdateLocationValidators
{
    public static ValidationResult Validate(this UpdateLocationDto dto)
    {
        var errors = new List<string>();

        // Validation for Longitude
        if (dto.Longitude < -180 || dto.Longitude > 180)
            errors.Add("Longitude must be between -180 and 180 degrees.");

        // Validation for Latitude
        if (dto.Latitude < -90 || dto.Latitude > 90)
            errors.Add("Latitude must be between -90 and 90 degrees.");

        // Validation for Altitude (assuming a realistic range)
        if (dto.Altitude < -500 || dto.Altitude > 9000) 
            errors.Add("Altitude must be a realistic value between -500 and 9000 meters.");

        // Validation for Depth
        if (dto.Depth < -12000 || dto.Depth > 0) 
            errors.Add("Depth must be a realistic value between -12000 and 0 meters.");
        
        // Validation for required foreign keys
        if (dto.TimezoneId == Guid.Empty)
            errors.Add("TimezoneId is required.");

        if (dto.SurfaceTypeId == Guid.Empty)
            errors.Add("SurfaceTypeId is required.");

        if (dto.ClimateZoneId == Guid.Empty)
            errors.Add("ClimateZoneId is required.");

        if (dto.NaturalFeatureId == Guid.Empty)
            errors.Add("NaturalFeatureId is required.");

        // Validation for Version
        if (dto.Version == Guid.Empty)
            errors.Add("Invalid Version. The GUID must not be empty.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}