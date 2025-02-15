// // packages
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Conventions;

// // source
// using server.src.Domain.Models.Errors;
// using server.src.Persistence.Configurations.Metrics;
// using server.src.Persistence.Contexts;

// namespace server.test.Persistence.Configurations.Metrics;

// public class LogErrorConfigurationTests
// {
//     private readonly string _dbName = "LogErrorTestDatabase";

//     private DbContextOptions<DataContext> GetInMemoryDbContextOptions()
//     {
//         // Return in-memory database options for testing purposes
//         return new DbContextOptionsBuilder<DataContext>()
//             .UseInMemoryDatabase(_dbName) // Ensure the package is installed and using the correct namespace
//             .Options;
//     }

//     private ModelBuilder GetModelBuilder()
//     {
//         var options = GetInMemoryDbContextOptions();
//         var context = new DataContext(options);
//         return new ModelBuilder(new ConventionSet());
//     }

//     [Fact]
//     public void Configure_ShouldSetPrimaryKeyForLogError()
//     {
//         // Arrange
//         var modelBuilder = GetModelBuilder();
//         var configuration = new LogErrorConfiguration();

//         // Act
//         configuration.Configure(modelBuilder.Entity<LogError>());

//         // Assert
//         var entity = modelBuilder.Model.FindEntityType(typeof(LogError));
//         var primaryKey = entity!.FindPrimaryKey();

//         // Check that the primary key is set on the "Id" property
//         Assert.NotNull(primaryKey);
//         Assert.Contains(primaryKey.Properties, p => p.Name == "Id");
//     }
// }