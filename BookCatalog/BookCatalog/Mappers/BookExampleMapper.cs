using BookCatalog.DTOs_Book;
using BookCatalog.DTOs_BookExample;
using BookCatalog.Models;

namespace BookCatalog.Mappers
{
    public static class BookExampleMapper
    {
        public static BookExampleDTO ToBookExampleDTO(this BookExample bookExample)
        {
            return new BookExampleDTO
            {
                BookExampleId = bookExample.BookExampleId,
                Title = bookExample.Title,
                Author = bookExample.Author,
                Genre = bookExample.Genre,
                Status = bookExample.Status
            };
        }
        public static BookExampleDetailDTO ToBookExampleDetailDTO(this BookExample bookExample)
        {
            return new BookExampleDetailDTO
            {
                Title = bookExample.Title,
                Author = bookExample.Author,
                Genre = bookExample.Genre
            };
        }
        public static BookExample ToBookExample(this BookExampleCreateDTO dto)
        {
            return new BookExample
            {
                Title = dto.Title,
                Author = dto.Author,
                Genre = dto.Genre
            };
        }
        public static BookExample ToBookExample(this BookExampleUpdateDTO dto)
        {
            return new BookExample
            {
                Title = dto.Title,
                Author = dto.Author,
                Genre = dto.Genre,
                Status = dto.Status
            };
        }
    }
}
