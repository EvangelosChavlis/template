// // source
// using server.src.Domain.Dto.Common;

// namespace server.test.Domain.Unit.Tests.Dto.Common;

// public class PaginatedListTests
// {
//     [Fact]
//     public void PaginatedList_ShouldInitializePropertiesCorrectly()
//     {
//         // Arrange
//         var paginatedList = new PaginatedList
//         {
//             PageNumber = 1,
//             PageSize = 10,
//             TotalRecords = 50
//         };

//         // Assert
//         Assert.Equal(1, paginatedList.PageNumber);
//         Assert.Equal(10, paginatedList.PageSize);
//         Assert.Equal(50, paginatedList.TotalRecords);
//         Assert.Equal(5, paginatedList.TotalPages); // 50 / 10 = 5
//     }

//     [Fact]
//     public void PaginatedList_ShouldCalculateTotalPages_WhenTotalRecordsIsDivisibleByPageSize()
//     {
//         // Arrange
//         var paginatedList = new PaginatedList
//         {
//             PageNumber = 1,
//             PageSize = 10,
//             TotalRecords = 100
//         };

//         // Assert
//         Assert.Equal(10, paginatedList.TotalPages); // 100 / 10 = 10
//     }

//     [Fact]
//     public void PaginatedList_ShouldCalculateTotalPages_WhenTotalRecordsIsNotDivisibleByPageSize()
//     {
//         // Arrange
//         var paginatedList = new PaginatedList
//         {
//             PageNumber = 1,
//             PageSize = 10,
//             TotalRecords = 105
//         };

//         // Assert
//         Assert.Equal(11, paginatedList.TotalPages); // 105 / 10 = 10.5 -> rounded up to 11
//     }

//     [Fact]
//     public void PaginatedList_ShouldReturnZeroTotalPages_WhenPageSizeIsZero()
//     {
//         // Arrange
//         var paginatedList = new PaginatedList
//         {
//             PageNumber = 1,
//             PageSize = 0,
//             TotalRecords = 50
//         };

//         // Assert
//         Assert.Equal(0, paginatedList.TotalPages); // Should return 0 to prevent division by zero
//     }

//     [Fact]
//     public void PaginatedList_ShouldReturnZeroTotalPages_WhenTotalRecordsIsZero()
//     {
//         // Arrange
//         var paginatedList = new PaginatedList
//         {
//             PageNumber = 1,
//             PageSize = 10,
//             TotalRecords = 0
//         };

//         // Assert
//         Assert.Equal(0, paginatedList.TotalPages); // 0 / 10 = 0
//     }
// }