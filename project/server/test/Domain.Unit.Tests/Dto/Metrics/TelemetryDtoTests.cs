// source
using server.src.Domain.Dto.Metrics;

namespace server.test.Domain.Unit.Tests.Dto.Metrics;

public class TelemetryDtoTests
{
    [Fact]
    public void Should_Create_Valid_ListItemTelemetryDto()
    {
        // Arrange
        var id = Guid.NewGuid();
        var method = "GET";
        var path = "/api/resource";
        var statusCode = "200";
        var responseTime = 120;
        var requestTimestamp = DateTime.UtcNow.ToString("o");

        // Act
        var dto = new ListItemTelemetryDto(id, method, path, statusCode, responseTime, requestTimestamp);

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(method, dto.Method);
        Assert.Equal(path, dto.Path);
        Assert.Equal(statusCode, dto.StatusCode);
        Assert.Equal(responseTime, dto.ResponseTime);
        Assert.Equal(requestTimestamp, dto.RequestTimestamp);
    }

    [Fact]
    public void Should_Create_Valid_ItemTelemetryDto()
    {
        // Arrange
        var id = Guid.NewGuid();
        var method = "POST";
        var path = "/api/resource";
        var statusCode = "201";
        var responseTime = 200;
        var memoryUsed = 50000;
        var cpuUsage = 12.5;
        var requestBodySize = 2048;
        var requestTimestamp = DateTime.UtcNow.ToString("o");
        var responseBodySize = 4096;
        var responseTimestamp = DateTime.UtcNow.AddSeconds(1).ToString("o");
        var clientIp = "192.168.0.1";
        var userAgent = "Mozilla/5.0";
        var threadId = "main";

        // Act
        var dto = new ItemTelemetryDto(id, method, path, statusCode, responseTime, memoryUsed, cpuUsage, 
                                        requestBodySize, requestTimestamp, responseBodySize, responseTimestamp, 
                                        clientIp, userAgent, threadId);

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(method, dto.Method);
        Assert.Equal(path, dto.Path);
        Assert.Equal(statusCode, dto.StatusCode);
        Assert.Equal(responseTime, dto.ResponseTime);
        Assert.Equal(memoryUsed, dto.MemoryUsed);
        Assert.Equal(cpuUsage, dto.CPUusage);
        Assert.Equal(requestBodySize, dto.RequestBodySize);
        Assert.Equal(requestTimestamp, dto.RequestTimestamp);
        Assert.Equal(responseBodySize, dto.ResponseBodySize);
        Assert.Equal(responseTimestamp, dto.ResponseTimestamp);
        Assert.Equal(clientIp, dto.ClientIp);
        Assert.Equal(userAgent, dto.UserAgent);
        Assert.Equal(threadId, dto.ThreadId);
    }
}