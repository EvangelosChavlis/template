// source
using server.src.Domain.Common.Models;

namespace server.src.Application.Auth.Users.Validators;

public static class TokenValidators
{
    public static ValidationResult ValidateToken(this string token)
    {
        var errors = new List<string>();

        // Validation for Email
        if (string.IsNullOrWhiteSpace(token))
            errors.Add("Email is required.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}