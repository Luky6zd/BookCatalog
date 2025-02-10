using BookCatalog.DTOs.DTOs_Book;
using BookCatalog.Models;

namespace BookCatalog.Mappers
{
    public static class BookMapper
    {
        // extension method converts Book to BookDTO for api response
        public static BookDTO ToBookDTO(this Book book)
        {
            return new BookDTO
            {
                BookId = book.BookId,
                Authors = book.Authors.Select(a => a.ToAuthorDTO()).ToList(),
                Title = book.Title,
                Genre = book.Genre,
                Status = book.Status
            };
        }

        // extension method converts Book to BookDetailDTO for api response
        public static BookDetailDTO ToBookDetailDTO(this Book book)
        { 
            return new BookDetailDTO
            {
                Title = book.Title,
                Genre = book.Genre,
                Status = book.Status,
                Year = book.Year,
                Authors = book.Authors.Select(author => author.ToAuthorDTO()).ToList()
            };
        }

        // extension method converts BookCreateDTO to Book entity for database
        public static Book ToBook(this BookCreateDTO dto)
        { 
            return new Book
            {
                Title = dto.Title,
                Genre = dto.Genre,
                //Authors = dto.Author.Select(author => author.ToAuthor()).ToList()
            };
        }

        // extension method converts BookUpdateDTO to Book entity for database
        public static Book ToBook(this BookUpdateDTO dto)
        { 
            return new Book
            {
                Title = dto.Title,
                Genre = dto.Genre,
                Status = dto.Status
            };
        }
    }
}
