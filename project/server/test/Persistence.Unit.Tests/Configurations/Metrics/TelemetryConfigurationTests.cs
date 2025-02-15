// // packages
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Conventions;

// // source
// using server.src.Domain.Models.Metrics;
// using server.src.Persistence.Configurations.Metrics;
// using server.src.Persistence.Contexts;

// namespace server.test.Persistence.Configurations.Metrics;

// public class TelemetryConfigurationTests
// {
//     private readonly string _dbName = "TelemetryTestDatabase";
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
//     public void Configure_ShouldSetPrimaryKeyForTelemetry()
//     {
//         // Arrange
//         var modelBuilder = GetModelBuilder();
//         var configuration = new TelemetryConfiguration();

//         // Act
//         configuration.Configure(modelBuilder.Entity<Telemetry>());

//         // Assert
//         var entity = modelBuilder.Model.FindEntityType(typeof(Telemetry));
//         var primaryKey = entity!.FindPrimaryKey();

//         // Check that the primary key is set on the "Id" property
//         Assert.NotNull(primaryKey);
//         Assert.Contains(primaryKey.Properties, p => p.Name == "Id");
//     }

//     [Fact]
//     public void Configure_ShouldSetMethodAsRequired()
//     {
//         // Arrange
//         var modelBuilder = GetModelBuilder();
//         var configuration = new TelemetryConfiguration();

//         // Act
//         configuration.Configure(modelBuilder.Entity<Telemetry>());

//         // Assert
//         var entity = modelBuilder.Model.FindEntityType(typeof(Telemetry));
//         var methodProperty = entity!.FindProperty("Method");

//         Assert.NotNull(methodProperty);
//         Assert.False(methodProperty.IsNullable);
//     }

//     [Fact]
//     public void Configure_ShouldSetPathAsRequired()
//     {
//         // Arrange
//         var modelBuilder = GetModelBuilder();
//         var configuration = new TelemetryConfiguration();

//         // Act
//         configuration.Configure(modelBuilder.Entity<Telemetry>());

//         // Assert
//         var entity = modelBuilder.Model.FindEntityType(typeof(Telemetry));
//         var pathProperty = entity!.FindProperty("Path");

//         Assert.NotNull(pathProperty);
//         Assert.False(pathProperty.IsNullable);
//     }

//     [Fact]
//     public void Configure_ShouldSetStatusCodeAsRequired()
//     {
//         // Arrange
//         var modelBuilder = GetModelBuilder();
//         var configuration = new TelemetryConfiguration();

//         // Act
//         configuration.Configure(modelBuilder.Entity<Telemetry>());

//         // Assert
//         var entity = modelBuilder.Model.FindEntityType(typeof(Telemetry));
//         var statusCodeProperty = entity!.FindProperty("StatusCode");

//         Assert.NotNull(statusCodeProperty);
//         Assert.False(statusCodeProperty.IsNullable);
//     }

//     [Fact]
//     public void Configure_ShouldSetResponseTimeAsRequired()
//     {
//         // Arrange
//         var modelBuilder = GetModelBuilder();
//         var configuration = new TelemetryConfiguration();

//         // Act
//         configuration.Configure(modelBuilder.Entity<Telemetry>());

//         // Assert
//         var entity = modelBuilder.Model.FindEntityType(typeof(Telemetry));
//         var responseTimeProperty = entity!.FindProperty("ResponseTime");

//         Assert.NotNull(responseTimeProperty);
//         Assert.False(responseTimeProperty.IsNullable);
//     }

//     [Fact]
//     public void Configure_ShouldSetMemoryUsedAsRequired()
//     {
//         // Arrange
//         var modelBuilder = GetModelBuilder();
//         var configuration = new TelemetryConfiguration();

//         // Act
//         configuration.Configure(modelBuilder.Entity<Telemetry>());

//         // Assert
//         var entity = modelBuilder.Model.FindEntityType(typeof(Telemetry));
//         var memoryUsedProperty = entity!.FindProperty("MemoryUsed");

//         Assert.NotNull(memoryUsedProperty);
//         Assert.False(memoryUsedProperty.IsNullable);
//     }

//     [Fact]
//     public void Configure_ShouldSetCPUusageAsRequired()
//     {
//         // Arrange
//         var modelBuilder = GetModelBuilder();
//         var configuration = new TelemetryConfiguration();

//         // Act
//         configuration.Configure(modelBuilder.Entity<Telemetry>());

//         // Assert
//         var entity = modelBuilder.Model.FindEntityType(typeof(Telemetry));
//         var cpuUsageProperty = entity!.FindProperty("CPUusage");

//         Assert.NotNull(cpuUsageProperty);
//         Assert.False(cpuUsageProperty.IsNullable);
//     }

//     [Fact]
//     public void Configure_ShouldSetRequestBodySizeAsRequired()
//     {
//         // Arrange
//         var modelBuilder = GetModelBuilder();
//         var configuration = new TelemetryConfiguration();

//         // Act
//         configuration.Configure(modelBuilder.Entity<Telemetry>());

//         // Assert
//         var entity = modelBuilder.Model.FindEntityType(typeof(Telemetry));
//         var requestBodySizeProperty = entity!.FindProperty("RequestBodySize");

//         Assert.NotNull(requestBodySizeProperty);
//         Assert.False(requestBodySizeProperty.IsNullable);
//     }

//     [Fact]
//     public void Configure_ShouldSetRequestTimestampAsRequired()
//     {
//         // Arrange
//         var modelBuilder = GetModelBuilder();
//         var configuration = new TelemetryConfiguration();

//         // Act
//         configuration.Configure(modelBuilder.Entity<Telemetry>());

//         // Assert
//         var entity = modelBuilder.Model.FindEntityType(typeof(Telemetry));
//         var requestTimestampProperty = entity!.FindProperty("RequestTimestamp");

//         Assert.NotNull(requestTimestampProperty);
//         Assert.False(requestTimestampProperty.IsNullable);
//     }

//     [Fact]
//     public void Configure_ShouldSetResponseBodySizeAsRequired()
//     {
//         // Arrange
//         var modelBuilder = GetModelBuilder();
//         var configuration = new TelemetryConfiguration();

//         // Act
//         configuration.Configure(modelBuilder.Entity<Telemetry>());

//         // Assert
//         var entity = modelBuilder.Model.FindEntityType(typeof(Telemetry));
//         var responseBodySizeProperty = entity!.FindProperty("ResponseBodySize");

//         Assert.NotNull(responseBodySizeProperty);
//         Assert.False(responseBodySizeProperty.IsNullable);
//     }

//     [Fact]
//     public void Configure_ShouldSetResponseTimestampAsRequired()
//     {
//         // Arrange
//         var modelBuilder = GetModelBuilder();
//         var configuration = new TelemetryConfiguration();

//         // Act
//         configuration.Configure(modelBuilder.Entity<Telemetry>());

//         // Assert
//         var entity = modelBuilder.Model.FindEntityType(typeof(Telemetry));
//         var responseTimestampProperty = entity!.FindProperty("ResponseTimestamp");

//         Assert.NotNull(responseTimestampProperty);
//         Assert.False(responseTimestampProperty.IsNullable);
//     }

//     [Fact]
//     public void Configure_ShouldSetClientIpAsRequired()
//     {
//         // Arrange
//         var modelBuilder = GetModelBuilder();
//         var configuration = new TelemetryConfiguration();

//         // Act
//         configuration.Configure(modelBuilder.Entity<Telemetry>());

//         // Assert
//         var entity = modelBuilder.Model.FindEntityType(typeof(Telemetry));
//         var clientIpProperty = entity!.FindProperty("ClientIp");

//         Assert.NotNull(clientIpProperty);
//         Assert.False(clientIpProperty.IsNullable);
//     }

//     [Fact]
//     public void Configure_ShouldSetUserAgentAsRequired()
//     {
//         // Arrange
//         var modelBuilder = GetModelBuilder();
//         var configuration = new TelemetryConfiguration();

//         // Act
//         configuration.Configure(modelBuilder.Entity<Telemetry>());

//         // Assert
//         var entity = modelBuilder.Model.FindEntityType(typeof(Telemetry));
//         var userAgentProperty = entity!.FindProperty("UserAgent");

//         Assert.NotNull(userAgentProperty);
//         Assert.False(userAgentProperty.IsNullable);
//     }

//     [Fact]
//     public void Configure_ShouldSetThreadIdAsRequired()
//     {
//         // Arrange
//         var modelBuilder = GetModelBuilder();
//         var configuration = new TelemetryConfiguration();

//         // Act
//         configuration.Configure(modelBuilder.Entity<Telemetry>());

//         // Assert
//         var entity = modelBuilder.Model.FindEntityType(typeof(Telemetry));
//         var threadIdProperty = entity!.FindProperty("ThreadId");

//         Assert.NotNull(threadIdProperty);
//         Assert.False(threadIdProperty.IsNullable);
//     }
// }