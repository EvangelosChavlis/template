// source
using server.src.Domain.Models.Errors;

namespace server.test.Domain.Unit.Tests.Models.Errors;

public class ErrorDetailsTests
{
    [Fact]
    public void ErrorDetails_Constructor_ShouldInitializePropertiesWithDefaults()
    {
        // Arrange & Act
        var errorDetails = new ErrorDetails();

        // Assert
        Assert.Equal(string.Empty, errorDetails.Error); // Default value for string
        Assert.Equal(0, errorDetails.StatusCode); // Default value for int
        Assert.Equal(string.Empty, errorDetails.Instance); // Default value for string
        Assert.Equal(string.Empty, errorDetails.ExceptionType); // Default value for string
        Assert.Equal(string.Empty, errorDetails.StackTrace); // Default value for string
        Assert.Equal(default(DateTime), errorDetails.Timestamp); // Default value for DateTime
    }

    [Fact]
    public void ErrorDetails_SetProperties_ShouldAssignValuesCorrectly()
    {
        // Arrange
        var timestamp = DateTime.Now;

        var errorDetails = new ErrorDetails
        {
            Error = "An error occurred",
            StatusCode = 500,
            Instance = "/api/resource",
            ExceptionType = "System.NullReferenceException",
            StackTrace = "at Namespace.Class.Method() in File:line 42",
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.Equal("An error occurred", errorDetails.Error); // Ensure Error is set
        Assert.Equal(500, errorDetails.StatusCode); // Ensure StatusCode is set
        Assert.Equal("/api/resource", errorDetails.Instance); // Ensure Instance is set
        Assert.Equal("System.NullReferenceException", errorDetails.ExceptionType); // Ensure ExceptionType is set
        Assert.Equal("at Namespace.Class.Method() in File:line 42", errorDetails.StackTrace); // Ensure StackTrace is set
        Assert.Equal(timestamp, errorDetails.Timestamp); // Ensure Timestamp is set
    }

    [Fact]
    public void ErrorDetails_ShouldAllowPropertyUpdates()
    {
        // Arrange
        var errorDetails = new ErrorDetails
        {
            Error = "Initial error",
            StatusCode = 400,
            Instance = "/api/initial",
            ExceptionType = "System.ArgumentException",
            StackTrace = "at Namespace.Initial.Method() in File:line 10",
            Timestamp = DateTime.Now.AddMinutes(-5)
        };

        var updatedTimestamp = DateTime.Now;

        // Act
        errorDetails.Error = "Updated error";
        errorDetails.StatusCode = 404;
        errorDetails.Instance = "/api/updated";
        errorDetails.ExceptionType = "System.InvalidOperationException";
        errorDetails.StackTrace = "at Namespace.Updated.Method() in File:line 20";
        errorDetails.Timestamp = updatedTimestamp;

        // Assert
        Assert.Equal("Updated error", errorDetails.Error); // Ensure Error is updated
        Assert.Equal(404, errorDetails.StatusCode); // Ensure StatusCode is updated
        Assert.Equal("/api/updated", errorDetails.Instance); // Ensure Instance is updated
        Assert.Equal("System.InvalidOperationException", errorDetails.ExceptionType); // Ensure ExceptionType is updated
        Assert.Equal("at Namespace.Updated.Method() in File:line 20", errorDetails.StackTrace); // Ensure StackTrace is updated
        Assert.Equal(updatedTimestamp, errorDetails.Timestamp); // Ensure Timestamp is updated
    }

    [Fact]
    public void ErrorDetails_Timestamp_ShouldReflectErrorOccurrenceTime()
    {
        // Arrange
        var occurrenceTime = DateTime.Now;
        var errorDetails = new ErrorDetails
        {
            Timestamp = occurrenceTime
        };

        // Act & Assert
        Assert.Equal(occurrenceTime, errorDetails.Timestamp); // Ensure Timestamp matches the occurrence time
    }
}
