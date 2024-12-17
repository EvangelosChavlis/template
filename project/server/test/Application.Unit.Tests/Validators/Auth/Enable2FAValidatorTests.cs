// packages
using FluentValidation.TestHelper;

// source
using server.src.Application.Validators.Auth;
using server.src.Domain.Dto.Auth;

namespace server.test.Application.Unit.Tests.Validators.Auth;

public class Enable2FAValidatorTests
{
    private readonly Enable2FAValidator _validator;

    public Enable2FAValidatorTests()
    {
        _validator = new Enable2FAValidator();
    }

    [Fact]
    public void Should_Have_Error_When_UserId_Is_Null()
    {
        // Arrange
        var dto = new Enable2FADto(null!);

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(e => e.UserId)
            .WithErrorMessage("UserId is required.");
    }

    [Fact]
    public void Should_Have_Error_When_UserId_Is_Empty()
    {
        // Arrange
        var dto = new Enable2FADto(string.Empty);

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(e => e.UserId)
            .WithErrorMessage("UserId is required.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_UserId_Is_Valid()
    {
        // Arrange
        var dto = new Enable2FADto("valid-user-id");

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldNotHaveValidationErrorFor(e => e.UserId);
    }
}