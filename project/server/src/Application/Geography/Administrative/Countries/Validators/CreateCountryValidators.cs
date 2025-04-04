// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Countries.Dtos;
using server.src.Domain.Geography.Administrative.Countries.Extensions;

namespace server.src.Application.Geography.Administrative.Countries.Validators;

public static class CreateCountryValidators
{
    public static ValidationResult Validate(this CreateCountryDto dto)
    {
        var errors = new List<string>();

       // Validation for Name
        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Name is required.");
        else if (dto.Name.Length > CountrySettings.NameLength)
            errors.Add($"Name {dto.Name} must not exceed {CountrySettings.NameLength} characters.");
        else if (dto.Name.ContainsInjectionCharacters())
            errors.Add($"Name {dto.Name} contains invalid characters.");
        else if (dto.Name.ContainsNonPrintableCharacters())
            errors.Add($"Name {dto.Name} contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(dto.Description))
            errors.Add("Description is required.");
        else if (dto.Description.Length > CountrySettings.DescriptionLength)
            errors.Add($"Description {dto.Description} must not exceed {CountrySettings.DescriptionLength} characters.");
        else if (dto.Description.ContainsInjectionCharacters())
            errors.Add($"Description {dto.Description} contains invalid characters.");
        else if (dto.Description.ContainsNonPrintableCharacters())
            errors.Add($"Description {dto.Description} contains non-printable characters.");

        // Validation for Code
        if (string.IsNullOrWhiteSpace(dto.Code))
            errors.Add("Code is required.");
        else if (dto.Code.Length > CountrySettings.CodeLength)
            errors.Add($"Code {dto.Code} must not exceed {CountrySettings.CodeLength} characters.");
        else if (dto.Code.ContainsInjectionCharacters())
            errors.Add($"Code {dto.Code} contains invalid characters.");
        else if (dto.Code.ContainsNonPrintableCharacters())
            errors.Add($"Code {dto.Code} contains non-printable characters.");

        // Validation for Code
        if (string.IsNullOrWhiteSpace(dto.Code))
            errors.Add("Code is required.");
        else if (dto.Code.Length > CountrySettings.CodeLength)
            errors.Add($"Code {dto.Code} must not exceed {CountrySettings.CodeLength} characters.");
        else if (dto.Code.ContainsInjectionCharacters())
            errors.Add($"Code {dto.Code} contains invalid characters.");
        else if (dto.Code.ContainsNonPrintableCharacters())
            errors.Add($"Code {dto.Code} contains non-printable characters.");

        // Validation for PhoneCode
        if (string.IsNullOrWhiteSpace(dto.PhoneCode))
            errors.Add("PhoneCode is required.");
        else if (dto.PhoneCode.Length > CountrySettings.PhoneCodeLength)
            errors.Add($"PhoneCode {dto.PhoneCode} must not exceed {CountrySettings.PhoneCodeLength} characters.");
        else if (dto.PhoneCode.ContainsInjectionCharacters())
            errors.Add($"PhoneCode {dto.PhoneCode} contains invalid characters.");
        else if (dto.Code.ContainsNonPrintableCharacters())
            errors.Add($"PhoneCode {dto.PhoneCode} contains non-printable characters.");

        // Validation for TLD
        if (string.IsNullOrWhiteSpace(dto.TLD))
            errors.Add("TLD is required.");
        else if (dto.TLD.Length > CountrySettings.TLDLength)
            errors.Add($"TLD {dto.TLD} must not exceed {CountrySettings.TLDLength} characters.");
        else if (dto.TLD.ContainsInjectionCharacters())
            errors.Add($"TLD {dto.TLD} contains invalid characters.");
        else if (dto.Code.ContainsNonPrintableCharacters())
            errors.Add($"TLD {dto.TLD} contains non-printable characters.");

        // Validation for Currency
        if (string.IsNullOrWhiteSpace(dto.Currency))
            errors.Add("Currency is required.");
        else if (dto.TLD.Length > CountrySettings.TLDLength)
            errors.Add($"Currency {dto.Currency} must not exceed {CountrySettings.CurrencyLength} characters.");
        else if (dto.TLD.ContainsInjectionCharacters())
            errors.Add($"Currency {dto.Currency} contains invalid characters.");
        else if (dto.Code.ContainsNonPrintableCharacters())
            errors.Add($"Currency {dto.Currency} contains non-printable characters.");

        // Validation for Population
        if (dto.Population < 0)
            errors.Add("Population cannot be negative.");

        // Validation for AreaKm2
        if (dto.AreaKm2 < 0)
            errors.Add("Area in square kilometers cannot be negative.");

        // Validation for ContinentId
        if (dto.ContinentId == Guid.Empty)
            errors.Add("ContinentId cannot be empty.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}