// source
using server.src.Domain.Models.Metrics;

namespace server.test.Domain.Unit.Tests.Models.Metrics;

public class TelemetryTests
{
    [Fact]
    public void Telemetry_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var telemetry = new Telemetry();

        // Assert
        Assert.Equal(Guid.Empty, telemetry.Id); // Default value for Guid
        Assert.Null(telemetry.Method); // Default value for string
        Assert.Null(telemetry.Path); // Default value for string
        Assert.Equal(0, telemetry.StatusCode); // Default value for int
        Assert.Equal(0, telemetry.ResponseTime); // Default value for long
        Assert.Equal(0, telemetry.MemoryUsed); // Default value for long
        Assert.Equal(0.0, telemetry.CPUusage); // Default value for double
        Assert.Equal(0, telemetry.RequestBodySize); // Default value for long
        Assert.Equal(default, telemetry.RequestTimestamp); // Default value for DateTime
        Assert.Equal(0, telemetry.ResponseBodySize); // Default value for long
        Assert.Equal(default, telemetry.ResponseTimestamp); // Default value for DateTime
        Assert.Null(telemetry.ClientIp); // Default value for string
        Assert.Null(telemetry.UserAgent); // Default value for string
        Assert.Null(telemetry.ThreadId); // Default value for string
    }

    [Fact]
    public void Telemetry_SetProperties_ShouldAssignValuesCorrectly()
    {
        // Arrange
        var telemetry = new Telemetry
        {
            Id = Guid.NewGuid(),
            Method = "GET",
            Path = "/api/data",
            StatusCode = 200,
            ResponseTime = 150,
            MemoryUsed = 50000,
            CPUusage = 35.5,
            RequestBodySize = 1024,
            RequestTimestamp = DateTime.Now.AddMinutes(-10),
            ResponseBodySize = 2048,
            ResponseTimestamp = DateTime.Now,
            ClientIp = "192.168.1.1",
            UserAgent = "Mozilla/5.0",
            ThreadId = "Thread-1"
        };

        // Act & Assert
        Assert.NotEqual(Guid.Empty, telemetry.Id); // Ensure Id is set
        Assert.Equal("GET", telemetry.Method); // Ensure Method is set
        Assert.Equal("/api/data", telemetry.Path); // Ensure Path is set
        Assert.Equal(200, telemetry.StatusCode); // Ensure StatusCode is set
        Assert.Equal(150, telemetry.ResponseTime); // Ensure ResponseTime is set
        Assert.Equal(50000, telemetry.MemoryUsed); // Ensure MemoryUsed is set
        Assert.Equal(35.5, telemetry.CPUusage); // Ensure CPUusage is set
        Assert.Equal(1024, telemetry.RequestBodySize); // Ensure RequestBodySize is set
        Assert.Equal(DateTime.Now.AddMinutes(-10).ToString(), telemetry.RequestTimestamp.ToString()); // Ensure RequestTimestamp is set
        Assert.Equal(2048, telemetry.ResponseBodySize); // Ensure ResponseBodySize is set
        Assert.Equal(DateTime.Now.ToString(), telemetry.ResponseTimestamp.ToString()); // Ensure ResponseTimestamp is set
        Assert.Equal("192.168.1.1", telemetry.ClientIp); // Ensure ClientIp is set
        Assert.Equal("Mozilla/5.0", telemetry.UserAgent); // Ensure UserAgent is set
        Assert.Equal("Thread-1", telemetry.ThreadId); // Ensure ThreadId is set
    }

    [Fact]
    public void Telemetry_ShouldBeAbleToChangeValues()
    {
        // Arrange
        var telemetry = new Telemetry
        {
            Id = Guid.NewGuid(),
            Method = "POST",
            Path = "/api/update",
            StatusCode = 400,
            ResponseTime = 200,
            MemoryUsed = 60000,
            CPUusage = 45.5,
            RequestBodySize = 2048,
            RequestTimestamp = DateTime.Now.AddMinutes(-5),
            ResponseBodySize = 4096,
            ResponseTimestamp = DateTime.Now.AddMinutes(-2),
            ClientIp = "10.0.0.1",
            UserAgent = "Chrome/91.0",
            ThreadId = "Thread-2"
        };

        // Act
        telemetry.Method = "PUT";
        telemetry.StatusCode = 201;
        telemetry.ResponseTime = 180;
        telemetry.MemoryUsed = 70000;

        // Assert
        Assert.Equal("PUT", telemetry.Method); // Ensure Method was updated
        Assert.Equal(201, telemetry.StatusCode); // Ensure StatusCode was updated
        Assert.Equal(180, telemetry.ResponseTime); // Ensure ResponseTime was updated
        Assert.Equal(70000, telemetry.MemoryUsed); // Ensure MemoryUsed was updated
    }

    [Fact]
    public void Telemetry_DateTimeProperties_ShouldBeSetCorrectly()
    {
        // Arrange
        var telemetry = new Telemetry
        {
            RequestTimestamp = DateTime.Now.AddMinutes(-30),
            ResponseTimestamp = DateTime.Now
        };

        // Act & Assert
        Assert.True(telemetry.RequestTimestamp < telemetry.ResponseTimestamp); // Ensure RequestTimestamp is earlier than ResponseTimestamp
    }
}
