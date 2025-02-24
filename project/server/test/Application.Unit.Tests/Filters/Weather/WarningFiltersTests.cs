// // source
// using server.src.Domain.Models.Weather;
// using server.src.Application.Filters.Weather;

// namespace server.test.Application.Unit.Tests.Filters.Weather;

// public class WarningFiltersTests
// {
//     [Fact]
//     public void WarningNameSorting_ShouldReturnCorrectPropertyName()
//     {
//         // Act
//         var result = WarningFilters.WarningNameSorting;

//         // Assert
//         Assert.Equal(nameof(Warning.Name), result);
//     }

//     [Theory]
//     [InlineData("Storm", true, false)]
//     [InlineData("Flood", false, true)]
//     [InlineData("Thunder", false, false)]
//     [InlineData("", true, true)]
//     [InlineData("Warning", true, true)]
//     public void WarningSearchFilter_ShouldFilterWarningsBasedOnInput(
//         string filter,
//         bool matchesFirstWarning,
//         bool matchesSecondWarning)
//     {
//         // Arrange
//         var warningData = new List<Warning>
//         {
//             new Warning
//             {
//                 Id = Guid.NewGuid(),
//                 Name = "Storm Warning",
//                 Description = "Severe storm expected in the area."
//             },
//             new Warning
//             {
//                 Id = Guid.NewGuid(),
//                 Name = "Flood Warning",
//                 Description = "Flooding expected due to heavy rain."
//             }
//         };

//         var searchFilter = filter.WarningSearchFilter();

//         // Act
//         var filteredWarnings = warningData.AsQueryable().Where(searchFilter).ToList();

//         // Assert
//         Assert.Equal(matchesFirstWarning, filteredWarnings.Any(w => w.Name == "Storm Warning"));
//         Assert.Equal(matchesSecondWarning, filteredWarnings.Any(w => w.Name == "Flood Warning"));
//     }
// }
