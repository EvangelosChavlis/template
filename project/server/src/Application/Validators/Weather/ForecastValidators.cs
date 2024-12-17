// packages
using FluentValidation;

// source
using server.src.Domain.Dto.Weather;

namespace server.src.Application.Validators.Forecast;

public class ForecastValidators : AbstractValidator<ForecastDto>
{
    public ForecastValidators()
    {
        RuleFor(f => f.Date)
            .NotNull()
            .WithMessage("Date is required.")
            .GreaterThan(DateTime.MinValue)
            .WithMessage("Date must be a valid date.");

        RuleFor(f => f.TemperatureC)
            .GreaterThanOrEqualTo(-50)
            .LessThanOrEqualTo(50)
            .WithMessage("TemperatureC must be between -50 and 50 degrees.");

        RuleFor(f => f.Summary)
            .NotNull()
            .NotEmpty()
            .WithMessage("Summary is required.")
            .MaximumLength(200)
            .WithMessage("Summary must not exceed 200 characters.");

        RuleFor(f => f.WarningId)
            .NotNull()
            .WithMessage("WarningId is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("WarningId must be a valid GUID.");
    }
}
