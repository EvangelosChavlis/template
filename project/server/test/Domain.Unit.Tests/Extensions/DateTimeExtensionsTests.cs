// source
using server.src.Domain.Extensions;

namespace server.tests.Domain.Extensions;

public class DateTimeExtensionsTests
{
    // Test the method GetLocalDateTimeString for nullable DateTime
    [Fact]
    public void GetLocalDateTimeString_FormatsDateCorrectly()
    {
        // Arrange
        DateTime? date = new DateTime(2023, 12, 4, 10, 5, 0, DateTimeKind.Utc); // UTC time

        // Act
        var result = date.GetLocalDateTimeString();

        // Assert
        Assert.Equal("04/12/2023 12:05", result);  // Adjust expected to reflect local time conversion (UTC+2 or UTC+3)
    }

    // Test the method GetFullLocalDateTimeString for nullable DateTime
    [Fact]
    public void GetFullLocalDateTimeString_FormatsFullDateTimeCorrectly()
    {
        // Arrange
        DateTime? date = new DateTime(2023, 12, 4, 10, 5, 0, DateTimeKind.Utc); // UTC time

        // Act
        var result = date.GetFullLocalDateTimeString();

        // Assert
        Assert.Equal("Monday 04/12/2023 12:05", result);  // Adjusted for local time
    }

    // Test the method GetLocalDate for non-nullable DateTime
    [Fact]
    public void GetLocalDate_ReturnsLocalDate()
    {
        // Arrange
        DateTime date = new DateTime(2023, 12, 4, 10, 5, 0, DateTimeKind.Utc); // UTC time

        // Act
        var result = date.GetLocalDate();

        // Assert
        Assert.Equal(new DateTime(2023, 12, 4, 12, 5, 0, DateTimeKind.Local), result); // Expected result after local time conversion
    }

    // Test the method GetLocalTimeString for nullable DateTime
    [Fact]
    public void GetLocalTimeString_FormatsTimeCorrectly()
    {
        // Arrange
        DateTime? date = new DateTime(2023, 12, 4, 10, 5, 0, DateTimeKind.Utc); // UTC time

        // Act
        var result = date.GetLocalTimeString();

        // Assert
        Assert.Equal("12:05", result);  // Adjusted for local time conversion
    }

    // Test the method GetUtcDate for non-nullable DateTime
    [Fact]
    public void GetUtcDate_ReturnsUtcDate()
    {
        // Arrange
        DateTime date = new DateTime(2023, 12, 4, 10, 5, 0, DateTimeKind.Local); // Local time

        // Act
        var result = date.GetUtcDate();

        // Assert
        Assert.Equal(new DateTime(2023, 12, 4, 08, 5, 0, DateTimeKind.Utc), result);  // Expected UTC result
    }
}
