// source
using server.src.Domain.Dto.Common;

namespace server.test.Domain.Unit.Tests.Dto.Common;

public class CommandResponseTests
{
    #region Default Behavior Tests

    [Fact]
    public void CommandResponse_ShouldInitializeWithNullDataAndFalseSuccess()
    {
        // Arrange & Act
        var commandResponse = new CommandResponse<string>();

        // Assert
        Assert.Null(commandResponse.Data);
        Assert.False(commandResponse.Success);
    }

    #endregion

    #region WithData Method Tests

    [Fact]
    public void WithData_ShouldSetDataProperty()
    {
        // Arrange
        var commandResponse = new CommandResponse<string>();
        var testData = "Test data";

        // Act
        commandResponse.WithData(testData);

        // Assert
        Assert.Equal(testData, commandResponse.Data);
    }

    [Fact]
    public void WithData_ShouldReturnSameObjectForMethodChaining()
    {
        // Arrange
        var commandResponse = new CommandResponse<string>();
        var testData = "Test data";

        // Act
        var result = commandResponse.WithData(testData);

        // Assert
        Assert.Same(commandResponse, result);  // Verifies method chaining returns the same object
    }

    #endregion

    #region WithSuccess Method Tests

    [Fact]
    public void WithSuccess_ShouldSetSuccessProperty()
    {
        // Arrange
        var commandResponse = new CommandResponse<string>();
        var success = true;

        // Act
        commandResponse.WithSuccess(success);

        // Assert
        Assert.True(commandResponse.Success);
    }

    [Fact]
    public void WithSuccess_ShouldReturnSameObjectForMethodChaining()
    {
        // Arrange
        var commandResponse = new CommandResponse<string>();
        var success = true;

        // Act
        var result = commandResponse.WithSuccess(success);

        // Assert
        Assert.Same(commandResponse, result);  // Verifies method chaining returns the same object
    }

    #endregion
}