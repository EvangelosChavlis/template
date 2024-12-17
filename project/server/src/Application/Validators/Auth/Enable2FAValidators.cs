// packages
using FluentValidation;

// source
using server.src.Domain.Dto.Auth;

namespace server.src.Application.Validators.Auth;

public class Enable2FAValidator : AbstractValidator<Enable2FADto>
{
    public Enable2FAValidator()
    {
        RuleFor(e => e.UserId)
            .NotNull()
            .NotEmpty()
            .WithMessage("UserId is required.");
    }
}
