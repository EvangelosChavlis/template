// packages
using System.Text.RegularExpressions;

// source
using server.src.Application.Common.Validators;
using server.src.Domain.Auth.Users.Dtos;
using server.src.Domain.Auth.Users.Extensions;
using server.src.Domain.Common.Models;

namespace server.src.Application.Auth.Users.Validators;

public static class CreateUserValidators
{
    public static ValidationResult Validate(this CreateUserDto dto)
    {
        var errors = new List<string>();

        // Validation for FirstName
        if (string.IsNullOrWhiteSpace(dto.FirstName))
            errors.Add("First name is required.");
        else if (dto.FirstName.Length > UserSettings.FirstNameLength)
            errors.Add($"First name {dto.FirstName} must not exceed {UserSettings.FirstNameLength} characters.");
        else if (dto.FirstName.ContainsInjectionCharacters())
            errors.Add($"First name {dto.FirstName} contains invalid characters.");
        else if (dto.FirstName.ContainsNonPrintableCharacters())
             errors.Add($"First name {dto.FirstName} contains non-printable characters.");
            
        // Validation for LastName
        if (string.IsNullOrWhiteSpace(dto.LastName))
            errors.Add("Last name is required.");
        else if (dto.LastName.Length > UserSettings.LastNameLength)
            errors.Add($"Last name {dto.LastName} must not exceed {UserSettings.LastNameLength} characters.");
        else if (dto.LastName.ContainsInjectionCharacters())
            errors.Add($"Last name {dto.LastName} contains invalid characters.");
        else if (dto.FirstName.ContainsNonPrintableCharacters())
            errors.Add($"Last name {dto.LastName} contains non-printable characters.");

        // Validation for Email
        if (string.IsNullOrWhiteSpace(dto.Email))
            errors.Add("Email is required.");
        else if (!Regex.IsMatch(dto.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            errors.Add("Invalid email format.");

        // Validation for UserName
        if (string.IsNullOrWhiteSpace(dto.UserName))
            errors.Add("Username is required.");
        else if (dto.UserName.Length > UserSettings.UserNameLength)
            errors.Add($"Username {dto.UserName} must not exceed {UserSettings.UserNameLength} characters.");
        else if (dto.LastName.ContainsNonPrintableCharacters())
            errors.Add($"Username {dto.UserName} contains invalid characters.");

        // Validation for Password
        if (string.IsNullOrWhiteSpace(dto.Password))
            errors.Add("Password is required.");
        else if (dto.Password.Length < UserSettings.PasswordMinLength || dto.Password.Length > UserSettings.PasswordMaxLength)
            errors.Add($"Password {dto.Password} must be between {UserSettings.PasswordMinLength} and {UserSettings.PasswordMaxLength} characters."); 
        else if (!Regex.IsMatch(dto.Password, @"[A-Z]"))
            errors.Add($"Password {dto.Password} must contain at least one uppercase letter.");
        else if (!Regex.IsMatch(dto.Password, @"[a-z]"))
            errors.Add($"Password {dto.Password} must contain at least one lowercase letter.");
        else if (!Regex.IsMatch(dto.Password, @"\d"))
            errors.Add($"Password {dto.Password} must contain at least one number.");
        else if (!Regex.IsMatch(dto.Password, @"[\W_]"))
            errors.Add($"Password {dto.Password} must contain at least one special character.");

        // Validation for Address
        if (string.IsNullOrWhiteSpace(dto.Address))
            errors.Add("Address is required.");
        else if (dto.Address.Length > UserSettings.AddressLength)
            errors.Add($"Address {dto.Address} must not exceed {UserSettings.AddressLength} characters.");

        // Validation for PhoneNumber
        if (string.IsNullOrWhiteSpace(dto.PhoneNumber))
            errors.Add("Phone number is required.");
        else if (!Regex.IsMatch(dto.PhoneNumber, @"^\+?\d{10,15}$"))
            errors.Add($"Phone number {dto.PhoneNumber} must be a valid international format.");

        // Validation for MobilePhoneNumber
        if (string.IsNullOrWhiteSpace(dto.MobilePhoneNumber))
            errors.Add("Mobile phone number is required.");
        else if (!Regex.IsMatch(dto.MobilePhoneNumber, @"^\+?\d{10,15}$"))
            errors.Add($"Mobile phone number {dto.MobilePhoneNumber} must be a valid international format.");

        // Validation for Bio
        if (dto.Bio?.Length > UserSettings.BioLength)
            errors.Add($"Bio {dto.Bio} must not exceed {UserSettings.BioLength} characters.");

        // Validation for DateOfBirth
        if (dto.DateOfBirth == default)
            errors.Add("Date of birth is required.");
        else if (dto.DateOfBirth > DateTime.Now.AddYears(-UserSettings.MinDateOfBirth))
            errors.Add($"User {dto.DateOfBirth} must be at least {UserSettings.MinDateOfBirth} years old.");

        // Validation for NeighborhoodId
        dto.NeighborhoodId.ValidateId();

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}