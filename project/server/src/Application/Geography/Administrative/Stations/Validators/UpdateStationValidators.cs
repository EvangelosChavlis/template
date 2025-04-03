// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Stations.Dtos;
using server.src.Domain.Geography.Administrative.Stations.Extensions;

namespace server.src.Application.Geography.Administrative.Stations.Validators;

public static class UpdateStationValidators
{
    public static ValidationResult Validate(this UpdateStationDto dto)
    {
        var errors = new List<string>();

        // Validation for Name
        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Name is required.");
        else if (dto.Name.Length > StationLength.NameLength)
            errors.Add($"Name {dto.Name} must not exceed {StationLength.NameLength} characters.");
        else if (dto.Name.ContainsInjectionCharacters())
            errors.Add($"Name {dto.Name} contains invalid characters.");
        else if (dto.Name.ContainsNonPrintableCharacters())
            errors.Add($"Name {dto.Name} contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(dto.Description))
            errors.Add("Description is required.");
        else if (dto.Description.Length > StationLength.DescriptionLength)
            errors.Add($"Description {dto.Description} must not exceed {StationLength.DescriptionLength} characters.");
        else if (dto.Description.ContainsInjectionCharacters())
            errors.Add($"Description {dto.Description} contains invalid characters.");
        else if (dto.Description.ContainsNonPrintableCharacters())
            errors.Add($"Description {dto.Description} contains non-printable characters.");

        // Validation for Code
        if (string.IsNullOrWhiteSpace(dto.Code))
            errors.Add("Code is required.");
        else if (dto.Code.Length > StationLength.CodeLength)
            errors.Add($"Code {dto.Code} must not exceed {StationLength.CodeLength} characters.");
        else if (dto.Code.ContainsInjectionCharacters())
            errors.Add($"Code {dto.Code} contains invalid characters.");
        else if (dto.Code.ContainsNonPrintableCharacters())
            errors.Add($"Code {dto.Code} contains non-printable characters.");

        // Validation for Longitude
        if (dto.Longitude < -180 || dto.Longitude > 180)
            errors.Add("Longitude must be between -180 and 180 degrees.");

        // Validation for Latitude
        if (dto.Latitude < -90 || dto.Latitude > 90)
            errors.Add("Latitude must be between -90 and 90 degrees.");

        // Validation for Version
        if (dto.Version == Guid.Empty)
            errors.Add("Invalid Version. The GUID must not be empty.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}