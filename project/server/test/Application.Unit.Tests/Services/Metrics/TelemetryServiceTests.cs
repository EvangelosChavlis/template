// // source
// using server.src.Application.Services.Metrics;
// using server.src.Domain.Exceptions;
// using server.src.Domain.Models.Common;
// using server.src.Domain.Models.Metrics;

// // test
// using server.test.Application.Unit.Tests.TestHelpers;

// namespace server.test.Application.Unit.Tests.Services.Metrics;

// public class TelemetryServiceTests : IClassFixture<ContextSetup>
// {
//     private readonly string _dbName = "TelemetryTestDb";
//     private readonly ContextSetup _contextSetup;

//     public TelemetryServiceTests(ContextSetup contextSetup)
//     {
//         _contextSetup = contextSetup;
//     }

//     [Fact]
//     public async Task GetTelemetryService_ShouldReturnPaginatedTelemetryList()
//     {
//         // Arrange
//         var context = _contextSetup.CreateDbContext(_dbName);
//         var commonRepository = _contextSetup.CreateCommonRepository();
//         var service = new TelemetryQueries(context, commonRepository);
//         await _contextSetup.ClearDatabase(context);

//         var telemetryData = new List<Telemetry>
//         {
//             new Telemetry
//             {
//                 Id = Guid.NewGuid(),
//                 Method = "GET",
//                 Path = "/api/test1",
//                 StatusCode = 200,
//                 ResponseTime = 123,
//                 MemoryUsed = 2048,
//                 CPUusage = 15.2,
//                 RequestBodySize = 512,
//                 RequestTimestamp = DateTime.UtcNow.AddMinutes(-10),
//                 ResponseBodySize = 1024,
//                 ResponseTimestamp = DateTime.UtcNow.AddMinutes(-9),
//                 ClientIp = "192.168.0.1",
//                 UserAgent = "TestAgent",
//                 ThreadId = "Thread-1"
//             },
//             new Telemetry
//             {
//                 Id = Guid.NewGuid(),
//                 Method = "POST",
//                 Path = "/api/test2",
//                 StatusCode = 500,
//                 ResponseTime = 300,
//                 MemoryUsed = 4096,
//                 CPUusage = 25.5,
//                 RequestBodySize = 1024,
//                 RequestTimestamp = DateTime.UtcNow.AddMinutes(-5),
//                 ResponseBodySize = 2048,
//                 ResponseTimestamp = DateTime.UtcNow.AddMinutes(-4),
//                 ClientIp = "192.168.0.2",
//                 UserAgent = "TestAgent2",
//                 ThreadId = "Thread-2"
//             }
//         };

//         context.TelemetryRecords.AddRange(telemetryData);
//         await context.SaveChangesAsync();

//         Assert.Equal(3, context.TelemetryRecords.Count()); // Ensure only added data exists

//         var pageParams = new UrlQuery
//         {
//             PageSize = 10,
//             PageNumber = 1
//         };

//         // Act
//         var result = await service.GetTelemetryService(pageParams);

//         // Assert
//         Assert.NotNull(result);
//         Assert.NotNull(result.Data);
//         Assert.Equal(3, result.Data.Count);
//         Assert.Equal("GET", result.Data.First().Method);
//     }


//     [Fact]
//     public async Task GetTelemetryByIdService_ShouldReturnCorrectTelemetry()
//     {
//         // Arrange
//         var context = _contextSetup.CreateDbContext(_dbName);
//         var commonRepository = _contextSetup.CreateCommonRepository();
//         var service = new TelemetryQueries(context, commonRepository);
//         await _contextSetup.ClearDatabase(context);

//         var telemetryRecord = new Telemetry
//         {
//             Id = Guid.NewGuid(),
//             Method = "GET",
//             Path = "/api/test",
//             StatusCode = 200,
//             ResponseTime = 150,
//             MemoryUsed = 1024,
//             CPUusage = 10.5,
//             RequestBodySize = 256,
//             RequestTimestamp = DateTime.UtcNow.AddMinutes(-15),
//             ResponseBodySize = 512,
//             ResponseTimestamp = DateTime.UtcNow.AddMinutes(-14),
//             ClientIp = "127.0.0.1",
//             UserAgent = "UnitTestAgent",
//             ThreadId = "Thread-123"
//         };

//         context.TelemetryRecords.Add(telemetryRecord);
//         await context.SaveChangesAsync();

//         // Act
//         var result = await service.GetTelemetryByIdService(telemetryRecord.Id);

//         // Assert
//         Assert.NotNull(result);
//         Assert.NotNull(result.Data);
//         Assert.Equal(telemetryRecord.Id, result.Data.Id);
//         Assert.Equal("GET", result.Data.Method);
//         Assert.Equal("/api/test", result.Data.Path);
//     }

//     [Fact]
//     public async Task GetTelemetryByIdService_ShouldThrowExceptionIfNotFound()
//     {
//         // Arrange
//         var context = _contextSetup.CreateDbContext(_dbName);
//         var commonRepository = _contextSetup.CreateCommonRepository();
//         var service = new TelemetryQueries(context, commonRepository);
//         await _contextSetup.ClearDatabase(context);

//         var nonExistentId = Guid.NewGuid();

//         // Act & Assert
//         await Assert.ThrowsAsync<CustomException>(() => service.GetTelemetryByIdService(nonExistentId));
//     }
// }
