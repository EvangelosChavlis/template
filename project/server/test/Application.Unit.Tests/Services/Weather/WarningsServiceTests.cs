// packages
using System.Net;
using Microsoft.EntityFrameworkCore;

// source
using server.src.Application.Services.Weather;
using server.src.Domain.Dto.Weather;
using server.src.Domain.Exceptions;
using server.src.Domain.Models.Common;
using server.src.Domain.Models.Weather;

// test
using server.test.Application.Unit.Tests.TestHelpers;

namespace server.test.Application.Unit.Tests.Services.Weather;

public class WarningsServiceTests : IClassFixture<ContextSetup> 
{
    private readonly string _dbName = "WarningsTestDb";
    private readonly ContextSetup _contextSetup;

    public WarningsServiceTests(ContextSetup contextSetup)
    {
        _contextSetup = contextSetup;
    }

    [Fact]
    public async Task GetWarningsService_ShouldReturnPagedWarnings()
    {
        // Arrange
        var context = _contextSetup.CreateDbContext(_dbName);
        var commonRepository = _contextSetup.CreateCommonRepository();
        var service = new WarningsService(context, commonRepository);

        await _contextSetup.ClearDatabase(context); // Ensure the database is empty before starting

        context.Warnings.AddRange(new List<Warning>
        {
            new() { 
                Id = Guid.NewGuid(), 
                Name = "Warning 1", 
                Description = "Description 1" 
            },
            new() { 
                Id = Guid.NewGuid(), 
                Name = "Warning 2", 
                Description = "Description 2" 
            }
        });
        await context.SaveChangesAsync();

        var pageParams = new UrlQuery { PageNumber = 1, PageSize = 10 };

        // Act
        var result = await service.GetWarningsService(pageParams);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Data!.Count);
        Assert.Equal(1, result.Pagination!.PageNumber);
        Assert.Equal(10, result.Pagination.PageSize);
    }

    [Fact]
    public async Task GetWarningsPickerService_ShouldReturnListOfWarnings()
    {
        // Arrange
        var context = _contextSetup.CreateDbContext(_dbName);
        var commonRepository = _contextSetup.CreateCommonRepository();
        var service = new WarningsService(context, commonRepository);

        await _contextSetup.ClearDatabase(context); // Ensure the database is empty before starting

        context.Warnings.AddRange(new List<Warning>
        {
            new() {
                Id = Guid.NewGuid(), 
                Name = "Warning 1", 
                Description = "Description 1" 
            },
            new() { 
                Id = Guid.NewGuid(), 
                Name = "Warning 2", 
                Description = "Description 2"
           }
        });
        await context.SaveChangesAsync();

        // Act
        var result = await service.GetWarningsPickerService();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Data!.Count);
    }

    [Fact]
    public async Task GetWarningByIdService_ShouldReturnWarning()
    {
        // Arrange
        var context = _contextSetup.CreateDbContext(_dbName);
        var commonRepository = _contextSetup.CreateCommonRepository();
        var service = new WarningsService(context, commonRepository);

        await _contextSetup.ClearDatabase(context); // Ensure the database is empty before starting

        var warning = new Warning 
        { 
            Id = Guid.NewGuid(), 
            Name = "Warning 1", 
            Description = "Description 1" 
        };
        context.Warnings.Add(warning);
        await context.SaveChangesAsync();

        // Act
        var result = await service.GetWarningByIdService(warning.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(warning.Name, result.Data!.Name);
        Assert.Equal(warning.Description, result.Data.Description);
    }

    [Fact]
    public async Task GetWarningByIdService_ShouldThrowCustomException_WhenWarningNotFound()
    {
        // Arrange
        var context = _contextSetup.CreateDbContext(_dbName);
        var commonRepository = _contextSetup.CreateCommonRepository();
        var service = new WarningsService(context, commonRepository);
        var invalidId = Guid.NewGuid(); // Ensure this ID doesn't exist in the database

        // Act & Assert
        var exception = await Assert.ThrowsAsync<CustomException>(() => service.GetWarningByIdService(invalidId));
        Assert.Equal("Warning not found", exception.Message);
        Assert.Equal((int)HttpStatusCode.NotFound, exception.StatusCode);
    }


    [Fact]
    public async Task InitializeWarningsService_ShouldInsertMultipleWarnings()
    {
        // Arrange
        var context = _contextSetup.CreateDbContext(_dbName);
        var commonRepository = _contextSetup.CreateCommonRepository();
        var service = new WarningsService(context, commonRepository);

        await _contextSetup.ClearDatabase(context); // Ensure the database is empty before starting

        var dtos = new List<WarningDto>
        {
            new (Name: "Warning 1", Description: "Description 1"),
            new (Name: "Warning 2", Description: "Description 2")
        };

        // Act
        var result = await service.InitializeWarningsService(dtos);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("Warning 1 inserted successfully!", result.Data);
        Assert.Contains("Warning 2 inserted successfully!", result.Data);
    }

    [Fact]
    public async Task CreateWarningService_ShouldInsertWarning()
    {
        // Arrange
        var context = _contextSetup.CreateDbContext(_dbName);
        var commonRepository = _contextSetup.CreateCommonRepository();
        var service = new WarningsService(context, commonRepository);

        await _contextSetup.ClearDatabase(context); // Ensure the database is empty before starting

        var dto = new WarningDto(Name: "Warning 1", Description: "Description 1");

        // Act
        var result = await service.CreateWarningService(dto);

        // Assert
        var warning = await context.Warnings
                                .Where(w => w.Name.Equals(dto.Name))
                                .FirstOrDefaultAsync();

        // Check if warning exists and matches the inserted data
        Assert.NotNull(warning);
        Assert.Equal(warning!.Name, dto.Name);
        Assert.Equal(warning!.Description, dto.Description);

        // Ensure the service's result message is correct
        Assert.Equal("Warning Warning 1 inserted successfully!", result.Data);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task CreateWarningService_ShouldThrowCustomException_WhenSaveFails()
    {
        // Arrange
        var context = _contextSetup.CreateDbContext(_dbName, simulateFailure: true); // Simulate failure in SaveChangesAsync
        var commonRepository = _contextSetup.CreateCommonRepository();
        var service = new WarningsService(context, commonRepository);

        var dto = new WarningDto(Name: "Warning 1", Description: "Description 1");

        // Act & Assert
        var exception = await Assert.ThrowsAsync<CustomException>(() => service.CreateWarningService(dto));

        // Verify that the exception message and status code are correct
        Assert.Equal("Failed to create warning.", exception.Message);
        Assert.Equal((int)HttpStatusCode.BadRequest, exception.StatusCode); // Ensure the status code is correct
    }



    [Fact]
    public async Task UpdateWarningService_ShouldUpdateWarning()
    {
        // Arrange
        var context = _contextSetup.CreateDbContext(_dbName);
        var commonRepository = _contextSetup.CreateCommonRepository();
        var service = new WarningsService(context, commonRepository);

        await _contextSetup.ClearDatabase(context); // Ensure the database is empty before starting

        var warning = new Warning { Id = Guid.NewGuid(), Name = "Warning 1", Description = "Description 1" };
        context.Warnings.Add(warning);
        await context.SaveChangesAsync();

        var dto = new WarningDto(Name: "Updated Warning", Description: "Updated Description");

        // Act
        var result = await service.UpdateWarningService(warning.Id, dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Warning Updated Warning updated successfully!", result.Data);
    }

    [Fact]
    public async Task UpdateWarningService_ShouldThrowCustomException_WhenWarningNotFound()
    {
        // Arrange
        var context = _contextSetup.CreateDbContext(_dbName);
        var commonRepository = _contextSetup.CreateCommonRepository();
        var service = new WarningsService(context, commonRepository);
        var invalidId = Guid.NewGuid(); // Ensure this ID doesn't exist in the database
        var dto = new WarningDto(Name: "Updated Warning", Description: "Updated Description");

        // Act & Assert
        var exception = await Assert.ThrowsAsync<CustomException>(() => service.UpdateWarningService(invalidId, dto));
        Assert.Equal("Warning not found", exception.Message);
        Assert.Equal((int)HttpStatusCode.NotFound, exception.StatusCode);
    }


    [Fact]
    public async Task DeleteWarningService_ShouldDeleteWarning()
    {
        // Arrange
        var context = _contextSetup.CreateDbContext(_dbName);
        var commonRepository = _contextSetup.CreateCommonRepository();
        var service = new WarningsService(context, commonRepository);

        await _contextSetup.ClearDatabase(context); // Ensure the database is empty before starting

        var warning = new Warning 
        { 
            Id = Guid.NewGuid(), 
            Name = "Warning 1", 
            Description = "Description 1" 
        };
        context.Warnings.Add(warning);
        await context.SaveChangesAsync();

        // Act
        var result = await service.DeleteWarningService(warning.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Warning Warning 1 deleted successfully!", result.Data);
    }

    [Fact]
    public async Task DeleteWarningService_ShouldThrowCustomException_WhenWarningNotFound()
    {
        // Arrange
        var context = _contextSetup.CreateDbContext(_dbName);
        var commonRepository = _contextSetup.CreateCommonRepository();
        var service = new WarningsService(context, commonRepository);
        var invalidId = Guid.NewGuid(); // Ensure this ID doesn't exist in the database

        // Act & Assert
        var exception = await Assert.ThrowsAsync<CustomException>(() => service.DeleteWarningService(invalidId));
        Assert.Equal("Warning not found", exception.Message);
        Assert.Equal((int)HttpStatusCode.NotFound, exception.StatusCode);
    }
}
