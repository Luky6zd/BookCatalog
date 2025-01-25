using BookCatalog.DTOs_Book;
using BookCatalog.Models;

namespace BookCatalog.Mappers
{
    public static class BookMapper
    {
        public static BookDTO ToBookDTO(this Book book)
        {
            return new BookDTO
            {
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                Status = book.Status
            };
        }
        public static BookDetailDTO ToBookDetailDTO(this Book book)
        {
            return new BookDetailDTO
            {
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                Status = book.Status,
                Year = book.Year
            };
        }

        public static Book ToBook(this BookCreateDTO dto)
        {
            return new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                Genre = dto.Genre
            };
        }

        public static Book ToBook(this BookUpdateDTO dto)
        {
            return new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                Genre = dto.Genre,
                Status = dto.Status
            };
        }
    }
}
