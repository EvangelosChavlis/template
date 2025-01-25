// source
using server.src.Domain.Dto.Auth;
using server.src.Domain.Models.Auth;
using server.src.Domain.Models.Errors;

namespace server.src.Application.Auth.Roles.Validators;

public static class RoleValidators
{
    public static ValidationResult Validate(RoleDto dto)
    {
        var errors = new List<string>();

        // Validation for Name
        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Name is required.");
        else if (dto.Name.Length > 100)
            errors.Add("Name must not exceed 100 characters.");
        else if (ContainsInjectionCharacters(dto.Name))
            errors.Add("Name contains invalid characters.");
        else if (ContainsNonPrintableCharacters(dto.Name))
            errors.Add("Name contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(dto.Description))
            errors.Add("Description is required.");
        else if (dto.Description.Length > 250)
            errors.Add("Description must not exceed 250 characters.");
        else if (ContainsInjectionCharacters(dto.Description))
            errors.Add("Description contains invalid characters.");
        else if (ContainsNonPrintableCharacters(dto.Description))
            errors.Add("Description contains non-printable characters.");

        // Validation for Version
        if (dto.Version == Guid.Empty)
            errors.Add("Invalid Version. The GUID must not be empty.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }

    public static ValidationResult Validate(Role model)
    {
        var errors = new List<string>();

        // Validation for Name
        if (string.IsNullOrWhiteSpace(model.Name))
            errors.Add("Name is required.");
        else if (model.Name.Length > 100)
            errors.Add("Name must not exceed 100 characters.");
        else if (ContainsInjectionCharacters(model.Name))
            errors.Add("Name contains invalid characters.");
        else if (ContainsNonPrintableCharacters(model.Name))
            errors.Add("Name contains non-printable characters.");

        // Validation for Normalized Name
        if (string.IsNullOrWhiteSpace(model.NormalizedName))
            errors.Add("NormalizedName is required.");
        else if (model.NormalizedName.Length > 100)
            errors.Add("NormalizedName must not exceed 100 characters.");
        else if (!model.NormalizedName.All(char.IsUpper))
            errors.Add("NormalizedName must be in capital letters.");
        else if (ContainsInjectionCharacters(model.NormalizedName))
            errors.Add("NormalizedName contains invalid characters.");
        else if (ContainsNonPrintableCharacters(model.NormalizedName))
            errors.Add("NormalizedName contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(model.Description))
            errors.Add("Description is required.");
        else if (model.Description.Length > 250)
            errors.Add("Description must not exceed 250 characters.");
        else if (ContainsInjectionCharacters(model.Description))
            errors.Add("Description contains invalid characters.");
        else if (ContainsNonPrintableCharacters(model.Description))
            errors.Add("Description contains non-printable characters.");

        // Validation for IsActive (boolean)
        if (model.IsActive is not true && model.IsActive is not false)
            errors.Add("IsActive must be either true or false.");

        // Validation for Version
        if (model.Version == Guid.Empty)
            errors.Add("Invalid Version. The GUID must not be empty.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }

    public static ValidationResult Validate(Guid id)
    {
        var errors = new List<string>();

        // Validation for Id
        if (id == Guid.Empty)
            errors.Add("Invalid ID. The GUID must not be empty.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }

    // Helper method to check for dangerous characters in input (SQL Injection prevention)
    private static bool ContainsInjectionCharacters(string input)
    {
        var dangerousChars = new[] { "'", ";", "--", "<", ">", 
            "/*", "*/", "=", "%", "@", "!", "#", "$", "^", "&" };
        return dangerousChars.Any(c => input.Contains(c));
    }

    // Helper method to check for non-printable characters (e.g., control characters)
    private static bool ContainsNonPrintableCharacters(string input)
    {
        return input.Any(c => char.IsControl(c));
    }
}
