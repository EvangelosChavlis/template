// source
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.Locations.Models;

namespace server.src.Application.Geography.Natural.Locations.Validators;

public static class LocationModelValidators
{
    public static ValidationResult Validate(this Location model)
    {
        var errors = new List<string>();

        // Validation for Longitude
        if (model.Longitude < -180 || model.Longitude > 180)
            errors.Add("Longitude must be between -180 and 180 degrees.");

        // Validation for Latitude
        if (model.Latitude < -90 || model.Latitude > 90)
            errors.Add("Latitude must be between -90 and 90 degrees.");

        // Validation for Altitude (assuming a realistic range)
        if (model.Altitude < -500 || model.Altitude > 9000) 
            errors.Add("Altitude must be a realistic value between -500 and 9000 meters.");

        // Validation for Depth
        if (model.Depth < -12000 || model.Depth > 0) 
            errors.Add("Depth must be a realistic value between -12000 and 0 meters.");

        // Validation for IsActive (boolean)
        if (model.IsActive is not true && model.IsActive is not false)
            errors.Add("IsActive must be either true or false.");
        
        // Validation for required foreign keys
        if (model.TimezoneId == Guid.Empty)
            errors.Add("TimezoneId is required.");

        if (model.SurfaceTypeId == Guid.Empty)
            errors.Add("SurfaceTypeId is required.");

        if (model.ClimateZoneId == Guid.Empty)
            errors.Add("ClimateZoneId is required.");
        
        if (model.NaturalFeatureId == Guid.Empty)
            errors.Add("NaturalFeatureId is required.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}