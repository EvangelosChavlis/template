// source
using server.src.Domain.Auth.Users.Models;
using server.src.Domain.Common.Models;

namespace server.src.Application.Auth.Users.Validators;

public static class UserModelValidators
{

    public static ValidationResult Validate(this User model)
    {
        var errors = new List<string>();

        // Validation for FirstName
        if (string.IsNullOrWhiteSpace(model.FirstName))
            errors.Add("First name is required.");

        // Validation for LastName
        if (string.IsNullOrWhiteSpace(model.LastName))
            errors.Add("Last name is required.");

        // Validation for Email
        if (string.IsNullOrWhiteSpace(model.Email))
            errors.Add("Email is required.");

        // Validation for UserName
        if (string.IsNullOrWhiteSpace(model.UserName))
            errors.Add("Username is required.");

        // Validation for PasswordHash
        if (string.IsNullOrWhiteSpace(model.PasswordHash))
            errors.Add("Password is required.");

        // Validation for Address
        if (string.IsNullOrWhiteSpace(model.Address))
            errors.Add("Address is required.");

        // Validation for ZipCode
        if (string.IsNullOrWhiteSpace(model.ZipCode))
            errors.Add("Zip code is required.");
    
        // Validation for PhoneNumber
        if (string.IsNullOrWhiteSpace(model.PhoneNumber))
            errors.Add("Phone number is required.");

        // Validation for MobilePhoneNumber
        if (string.IsNullOrWhiteSpace(model.MobilePhoneNumber))
            errors.Add("Mobile phone number is required.");

        // Validation for DateOfBirth
        if (string.IsNullOrWhiteSpace(model.DateOfBirth))
            errors.Add("Date of birth is required.");

        // Validation for IsActive (boolean)
        if (model.IsActive is not true && model.IsActive is not false)
            errors.Add("IsActive must be either true or false.");

        // Validation for Version
        if (model.Version == Guid.Empty)
            errors.Add("Invalid version. The GUID must not be empty.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}
