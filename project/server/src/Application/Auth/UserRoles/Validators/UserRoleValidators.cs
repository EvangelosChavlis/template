using server.src.Domain.Auth.UserRoles.Models;
using server.src.Domain.Common.Models;

namespace server.src.Application.Auth.UserRoles.Validators;

public static class UserRoleValidators
{
    public static ValidationResult Validate(UserRole model)
    {
        var errors = new List<string>();

        // Validation for UserId
        if (model.UserId == Guid.Empty)
            errors.Add("Invalid Version. The GUID must not be empty.");

        // Validation for RoleId
        if (model.RoleId == Guid.Empty)
            errors.Add("Invalid Version. The GUID must not be empty.");

        // Validation for DateOfBirth
        if (model.Date == default)
            errors.Add("Date is required.");
        // Return validation result
        return errors.Count > 0 ? 
            ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}