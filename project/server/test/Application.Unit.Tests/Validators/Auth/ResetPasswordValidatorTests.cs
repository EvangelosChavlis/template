// // packages
// using FluentValidation.TestHelper;

// // source
// using server.src.Application.Validators.Auth;
// using server.src.Domain.Dto.Auth;

// namespace server.test.Application.Unit.Tests.Validators.Auth;

// public class ResetPasswordValidatorTests
// {
//     private readonly ResetPasswordValidators _validator;

//     public ResetPasswordValidatorTests()
//     {
//         _validator = new ResetPasswordValidators();
//     }

//     // Email tests
//     [Fact]
//     public void Should_Have_Error_When_Email_Is_Null()
//     {
//         // Arrange
//         var dto = new ResetPasswordDto(
//             Email: null!, 
//             Token: "valid-token", 
//             NewPassword: "P@ssw0rd123"
//         );

//         // Act
//         var result = _validator.TestValidate(dto);

//         // Assert
//         result.ShouldHaveValidationErrorFor(r => r.Email)
//             .WithErrorMessage("Email is required.");
//     }

//     [Fact]
//     public void Should_Have_Error_When_Email_Is_Empty()
//     {
//         // Arrange
//         var dto = new ResetPasswordDto(
//             Email: string.Empty, 
//             Token: "valid-token", 
//             NewPassword: "P@ssw0rd123"
//         );

//         // Act
//         var result = _validator.TestValidate(dto);

//         // Assert
//         result.ShouldHaveValidationErrorFor(r => r.Email)
//             .WithErrorMessage("Email is required.");
//     }

//     [Fact]
//     public void Should_Have_Error_When_Email_Is_Invalid()
//     {
//         // Arrange
//         var dto = new ResetPasswordDto(
//             Email: "invalid-email", 
//             Token: "valid-token", 
//             NewPassword: "P@ssw0rd123"
//         );

//         // Act
//         var result = _validator.TestValidate(dto);

//         // Assert
//         result.ShouldHaveValidationErrorFor(r => r.Email)
//             .WithErrorMessage("Invalid email format.");
//     }

//     [Fact]
//     public void Should_Not_Have_Error_When_Email_Is_Valid()
//     {
//         // Arrange
//         var dto = new ResetPasswordDto(
//             Email: "valid.email@example.com", 
//             Token: "valid-token", 
//             NewPassword: "P@ssw0rd123");

//         // Act
//         var result = _validator.TestValidate(dto);

//         // Assert
//         result.ShouldNotHaveValidationErrorFor(r => r.Email);
//     }

//     // Token tests
//     [Fact]
//     public void Should_Have_Error_When_Token_Is_Null()
//     {
//         // Arrange
//         var dto = new ResetPasswordDto(
//             Email: "valid.email@example.com", 
//             Token: null!, 
//             NewPassword: "P@ssw0rd123"
//         );

//         // Act
//         var result = _validator.TestValidate(dto);

//         // Assert
//         result.ShouldHaveValidationErrorFor(r => r.Token)
//             .WithErrorMessage("Token is required.");
//     }

//     [Fact]
//     public void Should_Have_Error_When_Token_Is_Empty()
//     {
//         // Arrange
//         var dto = new ResetPasswordDto(
//             Email: "valid.email@example.com", 
//             Token: string.Empty, 
//             NewPassword: "P@ssw0rd123"
//         );

//         // Act
//         var result = _validator.TestValidate(dto);

//         // Assert
//         result.ShouldHaveValidationErrorFor(r => r.Token)
//             .WithErrorMessage("Token is required.");
//     }

//     // NewPassword tests
//     [Fact]
//     public void Should_Have_Error_When_NewPassword_Is_Null()
//     {
//         // Arrange
//         var dto = new ResetPasswordDto(
//             Email: "valid.email@example.com", 
//             Token: "valid-token", 
//             NewPassword: null!
//         );

//         // Act
//         var result = _validator.TestValidate(dto);

//         // Assert
//         result.ShouldHaveValidationErrorFor(r => r.NewPassword)
//             .WithErrorMessage("New password is required.");
//     }

//     [Fact]
//     public void Should_Have_Error_When_NewPassword_Is_Empty()
//     {
//         // Arrange
//         var dto = new ResetPasswordDto(
//             Email: "valid.email@example.com", 
//             Token: "valid-token", 
//             NewPassword: string.Empty
//         );

//         // Act
//         var result = _validator.TestValidate(dto);

//         // Assert
//         result.ShouldHaveValidationErrorFor(r => r.NewPassword)
//             .WithErrorMessage("New password is required.");
//     }

//     [Fact]
//     public void Should_Have_Error_When_NewPassword_Is_Too_Short()
//     {
//         // Arrange
//         var dto = new ResetPasswordDto(
//             Email: "valid.email@example.com", 
//             Token: "valid-token", 
//             NewPassword: "short"
//         );

//         // Act
//         var result = _validator.TestValidate(dto);

//         // Assert
//         result.ShouldHaveValidationErrorFor(r => r.NewPassword)
//             .WithErrorMessage("Password must be at least 8 characters.");
//     }

//     [Fact]
//     public void Should_Have_Error_When_NewPassword_Lacks_Uppercase()
//     {
//         // Arrange
//         var dto = new ResetPasswordDto(
//             Email: "valid.email@example.com", 
//             Token: "valid-token", 
//             NewPassword: "password123"
//         );

//         // Act
//         var result = _validator.TestValidate(dto);

//         // Assert
//         result.ShouldHaveValidationErrorFor(r => r.NewPassword)
//             .WithErrorMessage("Password must contain at least one uppercase letter.");
//     }

//     [Fact]
//     public void Should_Have_Error_When_NewPassword_Lacks_Lowercase()
//     {
//         // Arrange
//         var dto = new ResetPasswordDto(
//             Email: "valid.email@example.com", 
//             Token: "valid-token", 
//             NewPassword: "PASSWORD123"
//         );

//         // Act
//         var result = _validator.TestValidate(dto);

//         // Assert
//         result.ShouldHaveValidationErrorFor(r => r.NewPassword)
//             .WithErrorMessage("Password must contain at least one lowercase letter.");
//     }

//     [Fact]
//     public void Should_Have_Error_When_NewPassword_Lacks_Number()
//     {
//         // Arrange
//         var dto = new ResetPasswordDto(
//             Email: "valid.email@example.com", 
//             Token: "valid-token", 
//             NewPassword: "Password!"
//         );
        
//         // Act
//         var result = _validator.TestValidate(dto);

//         // Assert
//         result.ShouldHaveValidationErrorFor(r => r.NewPassword)
//             .WithErrorMessage("Password must contain at least one number.");
//     }

//     [Fact]
//     public void Should_Have_Error_When_NewPassword_Lacks_SpecialCharacter()
//     {
//         // Arrange
//         var dto = new ResetPasswordDto(
//             Email: "valid.email@example.com", 
//             Token: "valid-token", 
//             NewPassword: "Password123"
//         );

//         // Act
//         var result = _validator.TestValidate(dto);

//         // Assert
//         result.ShouldHaveValidationErrorFor(r => r.NewPassword)
//             .WithErrorMessage("Password must contain at least one special character.");
//     }

//     [Fact]
//     public void Should_Not_Have_Error_When_NewPassword_Is_Valid()
//     {
//         // Arrange
//         var dto = new ResetPasswordDto(
//             Email: "valid.email@example.com", 
//             Token: "valid-token", 
//             NewPassword: "P@ssw0rd123"
//         );

//         // Act
//         var result = _validator.TestValidate(dto);

//         // Assert
//         result.ShouldNotHaveValidationErrorFor(r => r.NewPassword);
//     }
// }
