using server.src.Domain.Common.Models;

namespace server.src.Application.Common.Validators;

public static class CommonValidators
{
    public static ValidationResult ValidateId(this Guid id)
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
    public static bool ContainsInjectionCharacters(this string input)
    {
        var dangerousChars = new[] { "'", ";", "--", "<", ">", 
            "/*", "*/", "=", "%", "@", "!", "#", "$", "^", "&" };
        return dangerousChars.Any(c => input.Contains(c));
    }

    // Helper method to check for non-printable characters (e.g., control characters)
    public static bool ContainsNonPrintableCharacters(this string input)
    {
        return input.Any(c => char.IsControl(c));
    }
}