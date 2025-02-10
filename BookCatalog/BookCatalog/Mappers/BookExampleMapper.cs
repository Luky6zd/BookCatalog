using BookCatalog.DTOs.DTOs_BookExample;
using BookCatalog.Models;

namespace BookCatalog.Mappers
{
    public static class BookExampleMapper
    {
        // extension method converts BookExample to BookExampleDTO for api response
        public static BookExampleDTO ToBookExampleDTO(this BookExample bookExample)
        {
            return new BookExampleDTO
            {
                BookExampleId = bookExample.BookExampleId,
                Title = bookExample.Book.Title,
                ISBN = bookExample.ISBN,
                Status = bookExample.Status,
                Genre = bookExample.Book.Genre
            };
        }

        // extension method converts BookExample to BookExampleDetailDTO for api response
        public static BookExampleDetailDTO ToBookExampleDetailDTO(this BookExample bookExample)
        {
            return new BookExampleDetailDTO
            {
                
            };
        }

        // extension method converts BookExampleCreateDTO to BookExample for database
        public static BookExample ToBookExample(this BookExampleCreateDTO dto)
        {
            return new BookExample
            {
               BookId = dto.BookId, 
               Status = dto.Status,
                ISBN = dto.ISBN
            };
        }
        // extension method converts BookExampleUpdateDTO to BookExample for database
        public static BookExample ToBookExample(this BookExampleUpdateDTO dto)
        {
            return new BookExample
            {
                Status = dto.Status
            };
        }
    }
}
