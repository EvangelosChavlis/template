// packages
using System.Net;
using Microsoft.EntityFrameworkCore;

// source
using server.src.Application.Services.Weather;
using server.src.Domain.Dto.Weather;
using server.src.Domain.Exceptions;
using server.src.Domain.Extensions;
using server.src.Domain.Models.Common;
using server.src.Domain.Models.Weather;

// test
using server.test.Application.Unit.Tests.TestHelpers;

namespace server.test.Application.Unit.Tests.Services.Weather;

public class ForecastsServiceTests : IClassFixture<ContextSetup> 
{
    private readonly string _dbName = "ForecastsTestDb";
    private readonly ContextSetup _contextSetup;

    public ForecastsServiceTests(ContextSetup contextSetup)
    {
        _contextSetup = contextSetup;
    }

    [Fact]
    public async Task GetForecastsService_ShouldReturnPagedForecasts()
    {
        // Arrange
        var context = _contextSetup.CreateDbContext(_dbName);
        var commonRepository = _contextSetup.CreateCommonRepository();
        var service = new ForecastsService(context, commonRepository);

        await _contextSetup.ClearDatabase(context);

        var lowWarning = new Warning 
        { 
            Id = Guid.NewGuid(), 
            Name = "Warning 1", 
            Description = "Description 1" 
        };
        context.Warnings.Add(lowWarning);

        var highWarning = new Warning 
        { 
            Id = Guid.NewGuid(), 
            Name = "Warning 1", 
            Description = "Description 1" 
        };
        context.Warnings.Add(highWarning);

        context.Forecasts.AddRange(new List<Forecast>
        {
            new() { 
                Id = Guid.NewGuid(), 
                Date = DateTime.UtcNow, 
                TemperatureC = 25, 
                Summary = "Clear skies",
                WarningId = lowWarning.Id 
            },
            new() { 
                Id = Guid.NewGuid(), 
                Date = DateTime.UtcNow.AddDays(1), 
                TemperatureC = 30,
                Summary = "Partly cloudy", 
                WarningId = highWarning.Id 
            }
        });
        context.SaveChanges();

        var pageParams = new UrlQuery { PageNumber = 1, PageSize = 10 };

        // Act
        var result = await service.GetForecastsService(pageParams);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Data!.Count);
        Assert.Equal(1, result.Pagination!.PageNumber);
        Assert.Equal(10, result.Pagination.PageSize);
    }


    [Fact]
    public async Task GetForecastByIdService_ShouldReturnForecast()
    {
        // Arrange
        var context = _contextSetup.CreateDbContext(_dbName);
        var commonRepository = _contextSetup.CreateCommonRepository();
        var service = new ForecastsService(context, commonRepository);

        await _contextSetup.ClearDatabase(context);

        var warning = new Warning 
        { 
            Id = Guid.NewGuid(), 
            Name = "Warning 1", 
            Description = "Description 1" 
        };
        context.Warnings.Add(warning);

        var forecast = new Forecast 
        { 
            Id = Guid.NewGuid(), 
            Date = DateTime.UtcNow, 
            TemperatureC = 25,
            Summary = "Mild and sunny",
            WarningId = warning.Id
        };

        context.Forecasts.Add(forecast);
        context.SaveChanges();

        // Act
        var result = await service.GetForecastByIdService(forecast.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(forecast.Date.GetFullLocalDateTimeString(), result.Data!.Date);
        Assert.Equal(forecast.TemperatureC, result.Data.TemperatureC);
    }

    [Fact]
    public async Task GetForecastByIdService_ShouldThrowCustomException_WhenForecastNotFound()
    {
        // Arrange
        var context = _contextSetup.CreateDbContext(_dbName);
        var commonRepository = _contextSetup.CreateCommonRepository();
        var service = new ForecastsService(context, commonRepository);
        await _contextSetup.ClearDatabase(context);
        var invalidId = Guid.NewGuid(); 

        // Act & Assert
        var exception = await Assert.ThrowsAsync<CustomException>(() => service.GetForecastByIdService(invalidId));
        Assert.Equal("Forecast not found", exception.Message);
        Assert.Equal((int)HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task InitializeForecastsService_ShouldInsertMultipleForecasts()
    {
        // Arrange
        var context = _contextSetup.CreateDbContext(_dbName);
        var commonRepository = _contextSetup.CreateCommonRepository();
        var service = new ForecastsService(context, commonRepository);

        await _contextSetup.ClearDatabase(context);

        var lowWarning = new Warning 
        { 
            Id = Guid.NewGuid(), 
            Name = "Low", 
            Description = "Low description" 
        };
        context.Warnings.Add(lowWarning);

        var highWarning = new Warning 
        { 
            Id = Guid.NewGuid(), 
            Name = "High", 
            Description = "High description" 
        };
        context.Warnings.Add(highWarning);

        context.SaveChanges();

        var dtos = new List<ForecastDto>
        {
            new (
                Date: DateTime.UtcNow, 
                TemperatureC: 25, 
                Summary: "Mild and sunny", 
                WarningId: lowWarning.Id
            ),
            new (
                Date: DateTime.UtcNow.AddDays(1), 
                TemperatureC: 30, 
                Summary: "Warm and partly cloudy", 
                WarningId: highWarning.Id
            )
        };


        // Act
        var result = await service.InitializeForecastsService(dtos);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task CreateForecastService_ShouldInsertForecast()
    {
        // Arrange
        var context = _contextSetup.CreateDbContext(_dbName);
        var commonRepository = _contextSetup.CreateCommonRepository();
        var service = new ForecastsService(context, commonRepository);

        await _contextSetup.ClearDatabase(context);

        var warning = new Warning
        {
            Id = Guid.NewGuid(),
            Name = "Low",
            Description = "Low description"
        };

        context.Warnings.Add(warning);
        context.SaveChanges();

        var dto = new ForecastDto(
            Date: DateTime.UtcNow, 
            TemperatureC: 25, 
            Summary: "Partly cloudy with mild temperatures",
            WarningId: warning.Id
        );



        // Act
        var result = await service.CreateForecastService(dto);

        // Assert
        var forecast = await context.Forecasts
            .Where(f => f.Date.Equals(dto.Date) && f.TemperatureC.Equals(dto.TemperatureC))
            .FirstOrDefaultAsync();

        Assert.NotNull(forecast);
        Assert.Equal(forecast!.Date, dto.Date);
        Assert.Equal(forecast!.TemperatureC, dto.TemperatureC);

        Assert.Equal($"Forecast {forecast.Date.GetLocalDateString()} inserted successfully!", result.Data);
    }

    [Fact]
    public async Task CreateForecastService_ShouldThrowCustomException_WhenSaveFails()
    {
        // Arrange
        var context = _contextSetup.CreateDbContext(_dbName, simulateFailure: true); 
        var commonRepository = _contextSetup.CreateCommonRepository();
        var service = new ForecastsService(context, commonRepository);
        await _contextSetup.ClearDatabase(context);

        // Add warning
        var lowWarning = new Warning 
        { 
            Id = Guid.NewGuid(), 
            Name = "Low", 
            Description = "Low description" 
        };
        context.Warnings.Add(lowWarning);
        context.SaveChanges();

        var dto = new ForecastDto(
            Date: DateTime.UtcNow, 
            TemperatureC: 25, 
            Summary: "Clear skies with moderate temperatures", // Added a summary
            WarningId: lowWarning.Id
        );

        // Act & Assert
        var exception = await Assert.ThrowsAsync<CustomException>(() => service.CreateForecastService(dto));
        Assert.Equal("Failed to create forecast.", exception.Message);
        Assert.Equal((int)HttpStatusCode.BadRequest, exception.StatusCode);
    }

    [Fact]
    public async Task UpdateForecastService_ShouldUpdateForecast()
    {
        // Arrange
        var context = _contextSetup.CreateDbContext(_dbName);
        var commonRepository = _contextSetup.CreateCommonRepository();
        var service = new ForecastsService(context, commonRepository);

        await _contextSetup.ClearDatabase(context);

        var lowWarning = new Warning 
        { 
            Id = Guid.NewGuid(), 
            Name = "Warning 1", 
            Description = "Description 1" 
        };
        context.Warnings.Add(lowWarning);

        var highWarning = new Warning 
        { 
            Id = Guid.NewGuid(), 
            Name = "Warning 2", 
            Description = "Description 2" 
        };
        context.Warnings.Add(highWarning);


        var forecast = new Forecast 
        { 
            Id = Guid.NewGuid(), 
            Date = DateTime.UtcNow, 
            TemperatureC = 25, 
            Summary = "Sunny",
            WarningId = lowWarning.Id
        };
        context.Forecasts.Add(forecast);
        context.SaveChanges();

        var dto = new ForecastDto(
            DateTime.UtcNow.AddDays(2), 
            TemperatureC: 35, 
            Summary: "Hot and sunny with high temperatures",
            WarningId: highWarning.Id
        );

        // Act
        var result = await service.UpdateForecastService(forecast.Id, dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal($"Forecast {forecast.Date.GetLocalDateString()} updated successfully!", result.Data);
    }

    [Fact]
    public async Task UpdateForecastService_ShouldThrowCustomException_WhenForecastNotFound()
    {
        // Arrange
        var context = _contextSetup.CreateDbContext(_dbName);
        var commonRepository = _contextSetup.CreateCommonRepository();
        var service = new ForecastsService(context, commonRepository);
        var invalidId = Guid.NewGuid(); 

        var highWarning = new Warning 
        { 
            Id = Guid.NewGuid(), 
            Name = "High", 
            Description = "High warning description" 
        };

        context.Warnings.Add(highWarning);
        context.SaveChanges();
        
        var dto = new ForecastDto(
            Date: DateTime.UtcNow.AddDays(2), 
            TemperatureC: 35, 
            Summary: "Hot and sunny with clear skies",
            WarningId: highWarning.Id
        );


        // Act & Assert
        var exception = await Assert.ThrowsAsync<CustomException>(() => service.UpdateForecastService(invalidId, dto));
        Assert.Equal("Forecast not found", exception.Message);
        Assert.Equal((int)HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task DeleteForecastService_ShouldDeleteForecast()
    {
        // Arrange
        var context = _contextSetup.CreateDbContext(_dbName);
        var commonRepository = _contextSetup.CreateCommonRepository();
        var service = new ForecastsService(context, commonRepository);

        await _contextSetup.ClearDatabase(context);

        var forecast = new Forecast 
        { 
            Id = Guid.NewGuid(), 
            Date = DateTime.UtcNow, 
            TemperatureC = 25, 
            Summary = "Clear skies",
            WarningId = Guid.NewGuid() 
        };
        context.Forecasts.Add(forecast);
        context.SaveChanges();

        // Act
        var result = await service.DeleteForecastService(forecast.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal($"Forecast {forecast.Date.GetLocalDateString()} deleted successfully!", result.Data);
    }

    [Fact]
    public async Task DeleteForecastService_ShouldThrowCustomException_WhenForecastNotFound()
    {
        // Arrange
        var context = _contextSetup.CreateDbContext(_dbName);
        var commonRepository = _contextSetup.CreateCommonRepository();
        var service = new ForecastsService(context, commonRepository);
        var invalidId = Guid.NewGuid(); 

        // Act & Assert
        var exception = await Assert.ThrowsAsync<CustomException>(() => service.DeleteForecastService(invalidId));
        Assert.Equal("Forecast not found", exception.Message);
        Assert.Equal((int)HttpStatusCode.NotFound, exception.StatusCode);
    }
}
