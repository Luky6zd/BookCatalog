using BookCatalog.Controllers;
using BookCatalog.DTOs.DTOs_BookExample;
using BookCatalog.Models;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using BookCatalog.Tests;


namespace BookCatalog.Tests;

public class BookController_UnitTests
{
    private readonly Mock<DataContext> _mockContext;
    private readonly BookExamplesController _controller;

    
    public BookController_UnitTests()
    {
        _mockContext = new Mock<DataContext>();
        _controller = new BookExamplesController(_mockContext.Object);
    }

    [Fact]
    public async Task GetBooks_ShouldReturnListOfBookDTOs()
    {
        var mockData = new List<Book>
        {
            new Book { BookId = 1, Title = "Title 1", Year = 2000, Genre = "Genre" },
            new Book { BookId = 2, Title = "Title 2", Year = 2010, Genre = "Genre" }
        }
        .AsQueryable();

        var mockDbSet = new Mock<DbSet<Book>>();
        mockDbSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(mockData.Provider);
        mockDbSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(mockData.Expression);
        mockDbSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(mockData.ElementType);
        mockDbSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(mockData.GetEnumerator());

        // Set up the _context to return the mock DbSet
        _mockContext.Setup(c => c.Books).Returns(mockDbSet.Object);

        // Act: Call the method
        var result = await _controller.GetBookExamples();

        // Assert: Verify the results
        var actionResult = Assert.IsType<ActionResult<IEnumerable<BookExampleDTO>>>(result);
        var returnValue = Assert.IsAssignableFrom<List<BookExampleDTO>>(actionResult.Value);

        // We expect 2 items in the result
        Assert.Equal(2, returnValue.Count); 
        Assert.Equal("Example 1", returnValue[0].Title);
        Assert.Equal("Example 2", returnValue[1].Title);


    }
}
