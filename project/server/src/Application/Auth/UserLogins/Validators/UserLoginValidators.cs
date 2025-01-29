// source
using server.src.Domain.Dto.Auth;
using server.src.Domain.Models.Auth;
using server.src.Domain.Models.Errors;

namespace server.src.Application.Auth.Roles.Validators;

public static class UserLoginValidators
{
    public static ValidationResult Validate(UserLoginDto dto)
    {
        var errors = new List<string>();

        // Validation for Username
        if (string.IsNullOrWhiteSpace(dto.Username))
            errors.Add("Username is required.");
        else if (ContainsInjectionCharacters(dto.Username))
            errors.Add("Username contains invalid characters.");
        else if (ContainsNonPrintableCharacters(dto.Username))
            errors.Add("Username contains non-printable characters.");

        // Validation for Password
        if (string.IsNullOrWhiteSpace(dto.Password))
            errors.Add("Password is required.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }

    public static ValidationResult Validate(UserLogin model)
    {
        var errors = new List<string>();

        // Validation for Date
        if (model.Date == default)
            errors.Add("Date is required.");

        // Validation for userId
        if (model.UserId == Guid.Empty)
            errors.Add("Invalid user id. The GUID must not be empty.");

        // Validation for LoginProvider
        if (string.IsNullOrWhiteSpace(model.LoginProvider))
            errors.Add("LoginProvider is required.");
        else if (model.LoginProvider.Length > 100)
            errors.Add("LoginProvider must not exceed 100 characters.");

        // Validation for Normalized ProviderDisplayName
        if (string.IsNullOrWhiteSpace(model.ProviderDisplayName))
            errors.Add("ProviderDisplayName is required.");

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
