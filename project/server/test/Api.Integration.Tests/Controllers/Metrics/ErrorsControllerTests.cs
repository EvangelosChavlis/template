// packages
using Newtonsoft.Json;

// source
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Metrics;
using server.src.Domain.Models.Errors;

// test
using server.test.Api.Integration.Tests.TestHelpers;

namespace server.test.Api.Integration.Tests.Controllers.Metrics;

public class ErrorsControllerTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly TestingWebAppFactory<Program> _factory;

    public ErrorsControllerTests(TestingWebAppFactory<Program> factory)
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
        var error = new LogError
        {
            Error = "NullReferenceException",
            StatusCode = 500,
            Instance = "Instance1",
            ExceptionType = "System.NullReferenceException",
            StackTrace = "at Example.Method()",
            Timestamp = DateTime.UtcNow
        };
        context.LogErrors.Add(error);
        context.SaveChanges();
    }

    [Fact]
    public async Task GetErrors_ReturnsListOfErrors()
    {
        // Arrange: Clear and seed the database before the test
        ClearAndSeedDatabase();

        // Act
        var response = await _client.GetAsync("/api/metrics/errors");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ListResponse<List<ListItemErrorDto>>>(content);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Data!);
        Assert.Contains(result.Data!, error => error.Error == "NullReferenceException");
    }

    [Fact]
    public async Task GetErrors_ReturnsEmptyIfNoErrors()
    {
        // Arrange: Clear the database to ensure no errors exist
        var context = _factory.GetDataContext();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        // Act
        var response = await _client.GetAsync("/api/metrics/errors");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ListResponse<List<ListItemErrorDto>>>(content);

        Assert.NotNull(result);
        Assert.Empty(result.Data!);
    }

    [Fact]
    public async Task GetErrorById_ReturnsSpecificError()
    {
        // Arrange: Clear and seed the database before the test
        ClearAndSeedDatabase();

        var context = _factory.GetDataContext();
        var error = context.LogErrors.FirstOrDefault();

        // Ensure that the error exists
        Assert.NotNull(error);
        var errorId = error.Id;

        // Act
        var response = await _client.GetAsync($"/api/metrics/errors/{errorId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ItemResponse<ItemErrorDto>>(content);

        Assert.NotNull(result);
        Assert.Equal(errorId, result.Data!.Id);
        Assert.Equal("NullReferenceException", result.Data.Error);
        Assert.Equal(500, result.Data.StatusCode);
        Assert.Equal("Instance1", result.Data.Instance);
    }
}