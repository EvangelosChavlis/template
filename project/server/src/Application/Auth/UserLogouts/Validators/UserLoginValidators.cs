// source
using server.src.Domain.Auth.UserLogouts.Models;
using server.src.Domain.Common.Models;

namespace server.src.Application.Auth.Roles.Validators;

public static class UserLogoutValidators
{
    public static ValidationResult Validate(UserLogout model)
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

}