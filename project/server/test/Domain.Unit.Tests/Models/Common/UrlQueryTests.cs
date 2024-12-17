// source
using server.src.Domain.Models.Common;

namespace server.tests.Domain.Models.Common;

public class UrlQueryTests
{
    [Fact]
    public void UrlQuery_Constructor_SetsDefaultValues()
    {
        // Arrange & Act
        var urlQuery = new UrlQuery();

        // Assert
        Assert.Equal(1, urlQuery.PageNumber);  // Default PageNumber should be 1
        Assert.Equal(20, urlQuery.PageSize);  // Default PageSize should be 20
        Assert.Null(urlQuery.Filter);         // Default Filter should be null
        Assert.Null(urlQuery.SortBy);         // Default SortBy should be null
        Assert.False(urlQuery.SortDescending); // Default SortDescending should be false
        Assert.False(urlQuery.HasFilter);      // Default HasFilter should be false
        Assert.False(urlQuery.HasSortBy);      // Default HasSortBy should be false
        Assert.Equal(0, urlQuery.TotalRecords);  // Default TotalRecords should be 0
    }

    [Fact]
    public void UrlQuery_HasFilter_ReturnsTrue_WhenFilterIsNotNullOrEmpty()
    {
        // Arrange
        var urlQuery = new UrlQuery { Filter = "SomeFilter" };

        // Act & Assert
        Assert.True(urlQuery.HasFilter);  // HasFilter should return true when Filter is not null or empty
    }

    [Fact]
    public void UrlQuery_HasFilter_ReturnsFalse_WhenFilterIsNullOrEmpty()
    {
        // Arrange
        var urlQuery1 = new UrlQuery { Filter = null };
        var urlQuery2 = new UrlQuery { Filter = string.Empty };

        // Act & Assert
        Assert.False(urlQuery1.HasFilter);  // HasFilter should return false when Filter is null
        Assert.False(urlQuery2.HasFilter);  // HasFilter should return false when Filter is empty
    }

    [Fact]
    public void UrlQuery_HasSortBy_ReturnsTrue_WhenSortByIsNotNullOrEmpty()
    {
        // Arrange
        var urlQuery = new UrlQuery { SortBy = "Name" };

        // Act & Assert
        Assert.True(urlQuery.HasSortBy);  // HasSortBy should return true when SortBy is not null or empty
    }

    [Fact]
    public void UrlQuery_HasSortBy_ReturnsFalse_WhenSortByIsNullOrEmpty()
    {
        // Arrange
        var urlQuery1 = new UrlQuery { SortBy = null };
        var urlQuery2 = new UrlQuery { SortBy = string.Empty };

        // Act & Assert
        Assert.False(urlQuery1.HasSortBy);  // HasSortBy should return false when SortBy is null
        Assert.False(urlQuery2.HasSortBy);  // HasSortBy should return false when SortBy is empty
    }

    [Fact]
    public void UrlQuery_PageNumberAndPageSize_AreMutable()
    {
        // Arrange
        var urlQuery = new UrlQuery();

        // Act
        urlQuery.PageNumber = 2;
        urlQuery.PageSize = 50;

        // Assert
        Assert.Equal(2, urlQuery.PageNumber);  // PageNumber should be updated to 2
        Assert.Equal(50, urlQuery.PageSize);   // PageSize should be updated to 50
    }

    [Fact]
    public void UrlQuery_SortDescending_DefaultsToFalse()
    {
        // Arrange & Act
        var urlQuery = new UrlQuery();

        // Assert
        Assert.False(urlQuery.SortDescending); // Default SortDescending should be false
    }

    [Fact]
    public void UrlQuery_SortDescending_CanBeSet()
    {
        // Arrange
        var urlQuery = new UrlQuery { SortDescending = true };

        // Act & Assert
        Assert.True(urlQuery.SortDescending);  // SortDescending should be set to true
    }

    [Fact]
    public void UrlQuery_TotalRecords_IsSetCorrectly()
    {
        // Arrange
        var urlQuery = new UrlQuery { TotalRecords = 100 };

        // Act & Assert
        Assert.Equal(100, urlQuery.TotalRecords); // Ensure TotalRecords is set to 100
    }
}