// // source
// using server.src.Domain.Exceptions;

// namespace server.test.Domain.Unit.Tests.Exceptions;

// public class CustomExceptionTests
// {
//     [Fact]
//     public void CustomException_ShouldSetMessageAndDefaultStatusCode()
//     {
//         // Arrange
//         var message = "An error occurred";

//         // Act
//         var exception = new CustomException(message);

//         // Assert
//         Assert.Equal(message, exception.Message); // Ensure the message is set correctly
//         Assert.Equal(400, exception.StatusCode); // Ensure the default status code is 400
//     }

//     [Fact]
//     public void CustomException_ShouldSetMessageAndCustomStatusCode()
//     {
//         // Arrange
//         var message = "Resource not found";
//         var statusCode = 404;

//         // Act
//         var exception = new CustomException(message, statusCode);

//         // Assert
//         Assert.Equal(message, exception.Message); // Ensure the message is set correctly
//         Assert.Equal(404, exception.StatusCode); // Ensure the custom status code is set
//     }

//     [Fact]
//     public void CustomException_ShouldInheritFromException()
//     {
//         // Arrange
//         var message = "An error occurred";
//         var statusCode = 500;

//         // Act
//         var exception = new CustomException(message, statusCode);

//         // Assert
//         Assert.IsAssignableFrom<Exception>(exception); // Check inheritance
//         Assert.Equal(message, exception.Message); // Ensure the message is set
//         Assert.Equal(statusCode, exception.StatusCode); // Ensure the status code is set
//     }

//     [Fact]
//     public void CustomException_ShouldHaveDefaultValuesForBaseExceptionProperties()
//     {
//         // Arrange
//         CustomException exception = null!;

//         try
//         {
//             // Act
//             throw new CustomException("Test exception");
//         }
//         catch (CustomException ex)
//         {
//             exception = ex;
//         }

//         // Assert
//         Assert.NotNull(exception); // Ensure the exception was caught
//         Assert.NotNull(exception.StackTrace); // Ensure the stack trace is populated
//         Assert.Equal("Test exception", exception.Message); // Verify the message
//     }
// }
