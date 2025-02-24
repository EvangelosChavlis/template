// // source
// using server.src.Domain.Models.Errors;
// using server.src.Application.Filters.Metrics;

// namespace server.test.Application.Unit.Tests.Filters.Metrics;

// public class ErrorsFiltersTests
// {
//     [Fact]
//     public void ErrorNameSorting_ShouldReturnCorrectPropertyName()
//     {
//         // Act
//         var result = ErrorsFiltrers.ErrorNameSorting;

//         // Assert
//         Assert.Equal(nameof(LogError.Error), result);
//     }

//     [Theory]
//     [InlineData("Error 1", true, false)]
//     [InlineData("500", true, false)]
//     [InlineData("Unknown", false, false)]
//     [InlineData("", true, true)]
//     public void ErrorSearchFilter_ShouldFilterErrorsBasedOnInput(
//         string filter,
//         bool matchesFirstError,
//         bool matchesSecondError)
//     {
//         // Arrange
//         var errors = new List<LogError>
//         {
//             new() {
//                 Id = Guid.NewGuid(),
//                 Error = "Error 1",
//                 StatusCode = 500,
//                 Timestamp = DateTime.UtcNow
//             },
//             new() {
//                 Id = Guid.NewGuid(),
//                 Error = "Error 2",
//                 StatusCode = 404,
//                 Timestamp = DateTime.UtcNow
//             }
//         };

//         var searchFilter = filter.ErrorSearchFilter();

//         // Act
//         var filteredErrors = errors.AsQueryable().Where(searchFilter).ToList();

//         // Assert
//         Assert.Equal(matchesFirstError, filteredErrors.Any(e => e.Error == "Error 1"));
//         Assert.Equal(matchesSecondError, filteredErrors.Any(e => e.Error == "Error 2"));
//     }
// }