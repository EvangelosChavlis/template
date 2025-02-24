// // source
// using server.src.Domain.Dto.Common;

// namespace server.test.Domain.Unit.Tests.Dto.Common;

// public class ListResponseTests
// {
//     #region Default Behavior Tests

//     [Fact]
//     public void ListResponse_ShouldInitializeWithNullDataAndPagination()
//     {
//         // Arrange & Act
//         var listResponse = new ListResponse<string>();

//         // Assert
//         Assert.Null(listResponse.Data);
//         Assert.Null(listResponse.Pagination);
//     }

//     #endregion

//     #region WithData Method Tests

//     [Fact]
//     public void WithData_ShouldSetDataProperty()
//     {
//         // Arrange
//         var listResponse = new ListResponse<string>();
//         var testData = "Test data";

//         // Act
//         listResponse.WithData(testData);

//         // Assert
//         Assert.Equal(testData, listResponse.Data);
//     }

//     [Fact]
//     public void WithData_ShouldReturnSameObjectForMethodChaining()
//     {
//         // Arrange
//         var listResponse = new ListResponse<string>();
//         var testData = "Test data";

//         // Act
//         var result = listResponse.WithData(testData);

//         // Assert
//         Assert.Same(listResponse, result);  // Verifies method chaining returns the same object
//     }

//     #endregion

//     #region Pagination Tests (Optional)

//     [Fact]
//     public void ListResponse_ShouldHandlePagination()
//     {
//         // Arrange
//         var listResponse = new ListResponse<string>();
//         var pagination = new PaginatedList { PageNumber = 1, PageSize = 10, TotalRecords = 50 };

//         // Act
//         listResponse.Pagination = pagination;

//         // Assert
//         Assert.Equal(1, listResponse.Pagination?.PageNumber);
//         Assert.Equal(10, listResponse.Pagination?.PageSize);
//         Assert.Equal(50, listResponse.Pagination?.TotalRecords);
//     }

//     #endregion
// }