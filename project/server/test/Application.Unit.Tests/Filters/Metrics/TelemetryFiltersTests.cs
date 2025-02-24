// // source
// using server.src.Domain.Models.Metrics;
// using server.src.Application.Filters.Metrics;

// namespace server.test.Application.Unit.Tests.Filters.Metrics;

// public class TelemetryFiltersTests
// {
//     [Fact]
//     public void TelemetryNameSorting_ShouldReturnCorrectPropertyName()
//     {
//         // Act
//         var result = TelemetryFiltrers.TelemetryNameSorting;

//         // Assert
//         Assert.Equal(nameof(Telemetry.Path), result);
//     }

//     [Theory]
//     [InlineData("GET", true, false)]
//     [InlineData("/api/test", true, false)]
//     [InlineData("200", true, false)]
//     [InlineData("", true, true)]
//     [InlineData("InvalidFilter", false, false)]
//     public void TelemetrySearchFilter_ShouldFilterTelemetryBasedOnInput(
//         string filter,
//         bool matchesFirstTelemetry,
//         bool matchesSecondTelemetry)
//     {
//         // Arrange
//         var telemetryData = new List<Telemetry>
//         {
//             new() {
//                 Id = Guid.NewGuid(),
//                 Method = "GET",
//                 Path = "/api/test",
//                 StatusCode = 200,
//                 ResponseTime = 123,
//                 RequestTimestamp = DateTime.UtcNow.AddMinutes(-10)
//             },
//             new() {
//                 Id = Guid.NewGuid(),
//                 Method = "POST",
//                 Path = "/api/another",
//                 StatusCode = 404,
//                 ResponseTime = 300,
//                 RequestTimestamp = DateTime.UtcNow.AddMinutes(-5)
//             }
//         };

//         var searchFilter = filter.TelemetrySearchFilter();

//         // Act
//         var filteredTelemetry = telemetryData.AsQueryable().Where(searchFilter).ToList();

//         // Assert
//         Assert.Equal(matchesFirstTelemetry, filteredTelemetry.Any(t => t.Method == "GET"));
//         Assert.Equal(matchesSecondTelemetry, filteredTelemetry.Any(t => t.Method == "POST"));
//     }
// }