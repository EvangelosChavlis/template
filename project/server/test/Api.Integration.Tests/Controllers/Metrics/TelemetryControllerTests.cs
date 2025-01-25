// packages
using Newtonsoft.Json;

// source
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Metrics;
using server.src.Domain.Models.Metrics;

// test
using server.test.Api.Integration.Tests.TestHelpers;

namespace server.test.Api.Integration.Tests.Controllers.Metrics;

public class TelemetryControllerTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly TestingWebAppFactory<Program> _factory;

    public TelemetryControllerTests(TestingWebAppFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    private void ClearAndSeedDatabase()
    {
        var context = _factory.GetDataContext();

        // Clear the database (no transaction needed)
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.SaveChanges();

        // Seed fresh data for each test
        var telemetry = new Telemetry
        {
            Id = Guid.NewGuid(),
            Method = "GET",
            Path = "/api/metrics/telemetry",
            StatusCode = 200,
            ResponseTime = 150,
            MemoryUsed = 1024,
            CPUusage = 0.5,
            RequestBodySize = 512,
            RequestTimestamp = DateTime.UtcNow.AddMinutes(-10),
            ResponseBodySize = 1024,
            ResponseTimestamp = DateTime.UtcNow,
            ClientIp = "192.168.0.1",
            UserAgent = "Mozilla/5.0",
            ThreadId = "12345"
        };

        context.TelemetryRecords.Add(telemetry);
        context.SaveChanges();
    }

    [Fact]
    public async Task GetTelemetry_ReturnsListOfTelemetryData()
    {
        // Arrange: Clear and seed the database before the test
        ClearAndSeedDatabase();

        // Act
        var response = await _client.GetAsync("/api/metrics/telemetry");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<List<ListItemTelemetryDto>>>(content);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Data!);
        Assert.Contains(result.Data!, item => item.Method == "GET");
    }

    [Fact]
    public async Task GetTelemetry_ReturnsEmptyIfNoTelemetryData()
    {
        // Arrange: Clear the database to ensure no telemetry data exists
        var context = _factory.GetDataContext();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        // Act
        var response = await _client.GetAsync("/api/metrics/telemetry");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<List<ListItemTelemetryDto>>>(content);

        Assert.NotNull(result);
        Assert.Empty(result.Data!);
    }

    [Fact]
    public async Task GetTelemetryById_ReturnsSpecificTelemetryData()
    {
        // Arrange: Clear and seed the database before the test
        ClearAndSeedDatabase();

        var context = _factory.GetDataContext();
        var telemetry = context.TelemetryRecords.FirstOrDefault();

        // Ensure that the telemetry data exists
        Assert.NotNull(telemetry);
        var telemetryId = telemetry.Id;

        // Act
        var response = await _client.GetAsync($"/api/metrics/telemetry/{telemetryId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ItemResponse<ItemTelemetryDto>>(content);

        Assert.NotNull(result);
        Assert.Equal(telemetryId, result.Data!.Id);
        Assert.Equal("GET", result.Data.Method);
        Assert.Equal("/api/metrics/telemetry", result.Data.Path);
        Assert.Equal(200.ToString(), result.Data.StatusCode);
        Assert.Equal(150, result.Data.ResponseTime);
        Assert.Equal(1024, result.Data.MemoryUsed);
        Assert.Equal("Mozilla/5.0", result.Data.UserAgent);
        Assert.Equal("12345", result.Data.ThreadId);
    }
}