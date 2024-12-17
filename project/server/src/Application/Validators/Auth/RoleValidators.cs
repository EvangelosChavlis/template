// packages
using FluentValidation;

// source
using server.src.Domain.Dto.Auth;

namespace server.src.Application.Validators.Role;

public class RoleValidators : AbstractValidator<RoleDto>
{
    public RoleValidators()
    {
        RuleFor(r => r.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100)
            .WithMessage("Name must not exceed 100 characters.");

        RuleFor(r => r.Description)
            .NotNull()
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(250)
            .WithMessage("Description must not exceed 250 characters.");
    }
}
