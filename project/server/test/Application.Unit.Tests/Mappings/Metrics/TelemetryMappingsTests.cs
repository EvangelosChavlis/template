// // source
// using server.src.Application.Mappings.Metrics;
// using server.src.Domain.Models.Metrics;
// using server.src.Domain.Extensions;

// namespace server.tests.Application.Mappings.Metrics;

// public class TelemetryMappingsTests
// {
//     [Fact]
//     public void ListItemTelemetryDtoMapping_MapsTelemetryToListItemTelemetryDto()
//     {
//         // Arrange
//         var telemetry = new Telemetry
//         {
//             Id = Guid.NewGuid(),
//             Method = "GET",
//             Path = "/api/resource",
//             StatusCode = 200,
//             ResponseTime = 123,
//             RequestTimestamp = new DateTime(2023, 12, 4, 10, 0, 0, DateTimeKind.Utc)
//         };

//         // Act
//         var result = telemetry.ListItemTelemetryDtoMapping();

//         // Assert
//         Assert.NotNull(result);
//         Assert.Equal(telemetry.Id, result.Id);
//         Assert.Equal(telemetry.Method, result.Method);
//         Assert.Equal(telemetry.Path, result.Path);
//         Assert.Equal(telemetry.StatusCode.ToString(), result.StatusCode);
//         Assert.Equal(telemetry.ResponseTime, result.ResponseTime);
//         Assert.Equal(telemetry.RequestTimestamp.GetFullLocalDateTimeString(), result.RequestTimestamp);
//     }

//     [Fact]
//     public void ItemTelemetryDtoMapping_MapsTelemetryToItemTelemetryDto()
//     {
//         // Arrange
//         var telemetry = new Telemetry
//         {
//             Id = Guid.NewGuid(),
//             Method = "POST",
//             Path = "/api/submit",
//             StatusCode = 201,
//             ResponseTime = 456,
//             MemoryUsed = 1024,
//             CPUusage = 25.6,
//             RequestBodySize = 512,
//             RequestTimestamp = new DateTime(2023, 12, 4, 10, 5, 0, DateTimeKind.Utc),
//             ResponseBodySize = 2048,
//             ResponseTimestamp = new DateTime(2023, 12, 4, 10, 5, 1, DateTimeKind.Utc),
//             ClientIp = "192.168.1.1",
//             UserAgent = "Mozilla/5.0",
//             ThreadId = "Thread-123"
//         };

//         // Act
//         var result = telemetry.ItemTelemetryDtoMapping();

//         // Assert
//         Assert.NotNull(result);
//         Assert.Equal(telemetry.Id, result.Id);
//         Assert.Equal(telemetry.Method, result.Method);
//         Assert.Equal(telemetry.Path, result.Path);
//         Assert.Equal(telemetry.StatusCode.ToString(), result.StatusCode);
//         Assert.Equal(telemetry.ResponseTime, result.ResponseTime);
//         Assert.Equal(telemetry.MemoryUsed, result.MemoryUsed);
//         Assert.Equal(telemetry.CPUusage, result.CPUusage);
//         Assert.Equal(telemetry.RequestBodySize, result.RequestBodySize);
//         Assert.Equal(telemetry.RequestTimestamp.GetFullLocalDateTimeString(), result.RequestTimestamp);
//         Assert.Equal(telemetry.ResponseBodySize, result.ResponseBodySize);
//         Assert.Equal(telemetry.ResponseTimestamp.GetFullLocalDateTimeString(), result.ResponseTimestamp);
//         Assert.Equal(telemetry.ClientIp, result.ClientIp);
//         Assert.Equal(telemetry.UserAgent, result.UserAgent);
//         Assert.Equal(telemetry.ThreadId, result.ThreadId);
//     }
// }