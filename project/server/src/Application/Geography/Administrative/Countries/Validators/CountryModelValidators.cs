// source
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Administrative.Countries.Extensions;
using server.src.Domain.Geography.Administrative.Countries.Models;

namespace server.src.Application.Geography.Administrative.Countries.Validators;

public static class CountryModelValidators
{
    public static ValidationResult Validate(this Country model)
    {
        var errors = new List<string>();

        // Validation for Name
        if (string.IsNullOrWhiteSpace(model.Name))
            errors.Add("Name is required.");
        else if (model.Name.Length > CountrySettings.NameLength)
            errors.Add($"Name {model.Name} must not exceed {CountrySettings.NameLength} characters.");
        else if (model.Name.ContainsInjectionCharacters())
            errors.Add($"Name {model.Name} contains invalid characters.");
        else if (model.Name.ContainsNonPrintableCharacters())
            errors.Add($"Name {model.Name} contains non-printable characters.");

        // Validation for Description
        if (string.IsNullOrWhiteSpace(model.Description))
            errors.Add("Description is required.");
        else if (model.Description.Length > CountrySettings.DescriptionLength)
            errors.Add($"Description {model.Description} must not exceed {CountrySettings.DescriptionLength} characters.");
        else if (model.Description.ContainsInjectionCharacters())
            errors.Add($"Description {model.Description} contains invalid characters.");
        else if (model.Description.ContainsNonPrintableCharacters())
            errors.Add($"Description {model.Description} contains non-printable characters.");

        // Validation for Code
        if (string.IsNullOrWhiteSpace(model.Code))
            errors.Add("Code is required.");
        else if (model.Code.Length > CountrySettings.CodeLength)
            errors.Add($"Code {model.Code} must not exceed {CountrySettings.CodeLength} characters.");
        else if (model.Code.ContainsInjectionCharacters())
            errors.Add($"Code {model.Code} contains invalid characters.");
        else if (model.Code.ContainsNonPrintableCharacters())
            errors.Add($"Code {model.Code} contains non-printable characters.");

        // Validation for PhoneCode
        if (string.IsNullOrWhiteSpace(model.PhoneCode))
            errors.Add("PhoneCode is required.");
        else if (model.PhoneCode.Length > CountrySettings.PhoneCodeLength)
            errors.Add($"PhoneCode {model.PhoneCode} must not exceed {CountrySettings.PhoneCodeLength} characters.");
        else if (model.PhoneCode.ContainsInjectionCharacters())
            errors.Add($"PhoneCode {model.PhoneCode} contains invalid characters.");
        else if (model.Code.ContainsNonPrintableCharacters())
            errors.Add($"PhoneCode {model.PhoneCode} contains non-printable characters.");

        // Validation for TLD
        if (string.IsNullOrWhiteSpace(model.TLD))
            errors.Add("TLD is required.");
        else if (model.TLD.Length > CountrySettings.TLDLength)
            errors.Add($"TLD {model.TLD} must not exceed {CountrySettings.TLDLength} characters.");
        else if (model.TLD.ContainsInjectionCharacters())
            errors.Add($"TLD {model.TLD} contains invalid characters.");
        else if (model.Code.ContainsNonPrintableCharacters())
            errors.Add($"TLD {model.TLD} contains non-printable characters.");

        // Validation for Currency
        if (string.IsNullOrWhiteSpace(model.Currency))
            errors.Add("Currency is required.");
        else if (model.TLD.Length > CountrySettings.TLDLength)
            errors.Add($"Currency {model.Currency} must not exceed {CountrySettings.CurrencyLength} characters.");
        else if (model.TLD.ContainsInjectionCharacters())
            errors.Add($"Currency {model.Currency} contains invalid characters.");
        else if (model.Code.ContainsNonPrintableCharacters())
            errors.Add($"Currency {model.Currency} contains non-printable characters.");

        // Validation for Population
        if (model.Population < 0)
            errors.Add("Population cannot be negative.");

        // Validation for AreaKm2
        if (model.AreaKm2 < 0)
            errors.Add("Area in square kilometers cannot be negative.");

        // Validation for IsActive (boolean)
        if (model.IsActive is not true && model.IsActive is not false)
            errors.Add("IsActive must be either true or false.");
        
        // Validation for ContinentId
        if (model.ContinentId == Guid.Empty)
            errors.Add("ContinentId cannot be empty.");

        // Validation for Version (ensure it's a valid GUID)
        if (model.Version == Guid.Empty)
            errors.Add("Invalid Version. The GUID must not be empty.");

        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}