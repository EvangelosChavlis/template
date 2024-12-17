// source
using server.src.Domain.Models.Errors;

namespace server.test.Domain.Unit.Tests.Models.Errors;

public class LogErrorTests
{
    [Fact]
    public void LogError_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var logError = new LogError();

        // Assert
        Assert.Equal(Guid.Empty, logError.Id); // Default value for Guid
        Assert.Null(logError.Error); // Default value for string
        Assert.Equal(0, logError.StatusCode); // Default value for int
        Assert.Null(logError.Instance); // Default value for string
        Assert.Null(logError.ExceptionType); // Default value for string
        Assert.Null(logError.StackTrace); // Default value for string
        Assert.Equal(default, logError.Timestamp); // Default value for DateTime
    }

    [Fact]
    public void LogError_SetProperties_ShouldAssignValuesCorrectly()
    {
        // Arrange
        var logError = new LogError
        {
            Id = Guid.NewGuid(),
            Error = "An unexpected error occurred.",
            StatusCode = 500,
            Instance = "/api/resource",
            ExceptionType = "System.NullReferenceException",
            StackTrace = "at Namespace.Class.Method() in File:line 42",
            Timestamp = DateTime.Now
        };

        // Act & Assert
        Assert.NotEqual(Guid.Empty, logError.Id); // Ensure Id is set
        Assert.Equal("An unexpected error occurred.", logError.Error); // Ensure Error is set
        Assert.Equal(500, logError.StatusCode); // Ensure StatusCode is set
        Assert.Equal("/api/resource", logError.Instance); // Ensure Instance is set
        Assert.Equal("System.NullReferenceException", logError.ExceptionType); // Ensure ExceptionType is set
        Assert.Equal("at Namespace.Class.Method() in File:line 42", logError.StackTrace); // Ensure StackTrace is set
        Assert.Equal(DateTime.Now.ToString(), logError.Timestamp.ToString()); // Ensure Timestamp is set
    }

    [Fact]
    public void LogError_ShouldAllowPropertyUpdates()
    {
        // Arrange
        var logError = new LogError
        {
            Id = Guid.NewGuid(),
            Error = "Initial error",
            StatusCode = 400,
            Instance = "/api/initial",
            ExceptionType = "System.ArgumentException",
            StackTrace = "at Namespace.Initial.Method() in File:line 10",
            Timestamp = DateTime.Now.AddMinutes(-5)
        };

        // Act
        logError.Error = "Updated error";
        logError.StatusCode = 404;
        logError.Instance = "/api/updated";
        logError.ExceptionType = "System.InvalidOperationException";
        logError.StackTrace = "at Namespace.Updated.Method() in File:line 20";
        logError.Timestamp = DateTime.Now;

        // Assert
        Assert.Equal("Updated error", logError.Error); // Ensure Error is updated
        Assert.Equal(404, logError.StatusCode); // Ensure StatusCode is updated
        Assert.Equal("/api/updated", logError.Instance); // Ensure Instance is updated
        Assert.Equal("System.InvalidOperationException", logError.ExceptionType); // Ensure ExceptionType is updated
        Assert.Equal("at Namespace.Updated.Method() in File:line 20", logError.StackTrace); // Ensure StackTrace is updated
        Assert.Equal(DateTime.Now.ToString(), logError.Timestamp.ToString()); // Ensure Timestamp is updated
    }

    [Fact]
    public void LogError_Timestamp_ShouldReflectErrorCreationTime()
    {
        // Arrange
        var creationTime = DateTime.Now;
        var logError = new LogError { Timestamp = creationTime };

        // Act & Assert
        Assert.Equal(creationTime, logError.Timestamp); // Ensure Timestamp matches the creation time
    }
}