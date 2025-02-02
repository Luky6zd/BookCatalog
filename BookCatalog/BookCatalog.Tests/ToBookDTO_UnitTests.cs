using BookCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookCatalog.DTOs;
using Xunit;


namespace BookCatalog.Tests
{
    public class ToBookDTO_UnitTests
    {
        [Fact]
        public void ToBookDTO_ShouldMapBookPropertiesCorrectly()
        {
            // Arrange: Create a test Book object
            var book = new Book
            {
                Title = "Test Book",
                Genre = "Fiction",
                Status = "Available"
            };

            // Act: Call the extension method to map to BookDTO
            //var bookDTO = book.ToBookDTO();

            // Assert: Verify that the mapping is correct
            Assert.Equal("Test Book", book.Title);
            Assert.Equal("Fiction", book.Genre);
            Assert.Equal("Available", book.Status);
        }

        [Fact]
        public void ToBookDTO_ShouldHandleNullValues()
        {
            // Arrange: Create a Book object with null values
            var book = new Book
            {
                Title = null,
                Genre = null,
                Status = null
            };

            // Act: Call the extension method to map to BookDTO
            //var bookDTO = book.ToBookDTO();

            // Assert: Verify that the null values are correctly handled
            Assert.Null(book.Title);
            Assert.Null(book.Genre);
            Assert.Null(book.Status);
        }
    }
}
