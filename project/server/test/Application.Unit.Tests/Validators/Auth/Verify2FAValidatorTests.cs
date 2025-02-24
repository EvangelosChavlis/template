// // packages
// using FluentValidation.TestHelper;

// // source
// using server.src.Application.Validators.Auth;
// using server.src.Domain.Dto.Auth;

// namespace server.test.Application.Unit.Tests.Validators.Auth;

// public class Verify2FAValidatorTests
// {
//     private readonly Verify2FAValidator _validator;

//     public Verify2FAValidatorTests()
//     {
//         _validator = new Verify2FAValidator();
//     }

//     // UserId tests
//     [Fact]
//     public void Should_Have_Error_When_UserId_Is_Null()
//     {
//         // Arrange
//         var dto = new Verify2FADto(UserId: null!, Token: "validToken123");

//         // Act
//         var result = _validator.TestValidate(dto);

//         // Assert
//         result.ShouldHaveValidationErrorFor(v => v.UserId)
//             .WithErrorMessage("UserId is required.");
//     }

//     [Fact]
//     public void Should_Have_Error_When_UserId_Is_Empty()
//     {
//         // Arrange
//         var dto = new Verify2FADto(UserId: string.Empty, Token: "validToken123");

//         // Act
//         var result = _validator.TestValidate(dto);

//         // Assert
//         result.ShouldHaveValidationErrorFor(v => v.UserId)
//             .WithErrorMessage("UserId is required.");
//     }

//     [Fact]
//     public void Should_Not_Have_Error_When_UserId_Is_Valid()
//     {
//         // Arrange
//         var dto = new Verify2FADto(UserId: "validUserId123", Token: "validToken123");

//         // Act
//         var result = _validator.TestValidate(dto);

//         // Assert
//         result.ShouldNotHaveValidationErrorFor(v => v.UserId);
//     }

//     // Token tests
//     [Fact]
//     public void Should_Have_Error_When_Token_Is_Null()
//     {
//         // Arrange
//         var dto = new Verify2FADto(UserId: "validUserId123", Token: null!);

//         // Act
//         var result = _validator.TestValidate(dto);

//         // Assert
//         result.ShouldHaveValidationErrorFor(v => v.Token)
//             .WithErrorMessage("Token is required.");
//     }

//     [Fact]
//     public void Should_Have_Error_When_Token_Is_Empty()
//     {
//         // Arrange
//         var dto = new Verify2FADto(UserId: "validUserId123", Token: string.Empty);

//         // Act
//         var result = _validator.TestValidate(dto);

//         // Assert
//         result.ShouldHaveValidationErrorFor(v => v.Token)
//             .WithErrorMessage("Token is required.");
//     }

//     [Fact]
//     public void Should_Not_Have_Error_When_Token_Is_Valid()
//     {
//         // Arrange
//         var dto = new Verify2FADto(UserId: "validUserId123", Token: "validToken123");

//         // Act
//         var result = _validator.TestValidate(dto);

//         // Assert
//         result.ShouldNotHaveValidationErrorFor(v => v.Token);
//     }
// }