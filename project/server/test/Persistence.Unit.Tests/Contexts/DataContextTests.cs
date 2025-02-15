// // packages
// using Microsoft.EntityFrameworkCore;

// // source
// using server.src.Domain.Models.Metrics;
// using server.src.Domain.Models.Weather;
// using server.src.Persistence.Contexts;
// using server.src.Domain.Models.Errors;

// namespace server.test.Persistence.Unit.Tests.Contexts;

// public class DataContextTests
// {
//     private readonly string _dbName = "TestDatabase";

//     private DbContextOptions<DataContext> GetInMemoryDbContextOptions()
//     {
//         return new DbContextOptionsBuilder<DataContext>()
//             .UseInMemoryDatabase(_dbName)
//             .Options;
//     }

//     [Fact]
//     public void Should_Have_Correct_DbSet_For_Forecasts()
//     {
//         // Arrange
//         var options = GetInMemoryDbContextOptions();
//         using var context = new DataContext(options);

//         // Act
//         var dbSet = context.Forecasts;

//         // Assert
//         Assert.NotNull(dbSet); // DbSet for Forecast should not be null
//     }

//     [Fact]
//     public void Should_Have_Correct_DbSet_For_Warnings()
//     {
//         // Arrange
//         var options = GetInMemoryDbContextOptions();
//         using var context = new DataContext(options);

//         // Act
//         var dbSet = context.Warnings;

//         // Assert
//         Assert.NotNull(dbSet); // DbSet for Warnings should not be null
//     }

//     [Fact]
//     public void Should_Have_Correct_DbSet_For_LogErrors()
//     {
//         // Arrange
//         var options = GetInMemoryDbContextOptions();
//         using var context = new DataContext(options);

//         // Act
//         var dbSet = context.LogErrors;

//         // Assert
//         Assert.NotNull(dbSet); // DbSet for LogErrors should not be null
//     }

//     [Fact]
//     public void Should_Have_Correct_DbSet_For_TelemetryRecords()
//     {
//         // Arrange
//         var options = GetInMemoryDbContextOptions();
//         using var context = new DataContext(options);

//         // Act
//         var dbSet = context.TelemetryRecords;

//         // Assert
//         Assert.NotNull(dbSet); // DbSet for TelemetryRecords should not be null
//     }

//     [Fact]
//     public void OnModelCreating_Should_Configure_Entities_Correctly()
//     {
//         // Arrange
//         var options = GetInMemoryDbContextOptions();
//         using var context = new DataContext(options);

//         // Act
//         var model = context.Model;

//         // Assert
//         var forecastEntity = model.FindEntityType(typeof(Forecast));
//         Assert.NotNull(forecastEntity); // Forecast entity should be configured

//         var warningEntity = model.FindEntityType(typeof(Warning));
//         Assert.NotNull(warningEntity); // Warning entity should be configured

//         var logErrorEntity = model.FindEntityType(typeof(LogError));
//         Assert.NotNull(logErrorEntity); // LogError entity should be configured

//         var telemetryEntity = model.FindEntityType(typeof(Telemetry));
//         Assert.NotNull(telemetryEntity); // Telemetry entity should be configured
//     }
// }
