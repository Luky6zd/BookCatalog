using BookCatalog.DTOs.DTOs_BookExample;
using BookCatalog.Models;

namespace BookCatalog.Mappers
{
    public static class BookExampleMapper
    {
        // extension method -> maping BookExample to BookExampleDTO
        // keyword this -> this method is going to be called on any BookExample object, as part of BookExample's class
        public static BookExampleDTO ToBookExampleDTO(this BookExample bookExample)
        {
            // returns object type BookExampleDTO
            // creates new BookExampleDTO object and assigns values from BookExample object
            // converts BookExample entity to BookExampleDTO for api response
            return new BookExampleDTO
            {
                BookExampleId = bookExample.BookExampleId, 
                Status = bookExample.Status
            };
        }

        // extension method -> maping BookExample to BookExampleDetailDTO
        public static BookExampleDetailDTO ToBookExampleDetailDTO(this BookExample bookExample)
        {
            // returns object type BookExampleDetailDTO
            // creates new BookExampleDetailDTO object and assigns values from BookExample object
            // converts BookExample entity to BookExampleDetailDTO for api response
            return new BookExampleDetailDTO
            {
                
            };
        }

        // extension method -> maping BookExampleCreateDTO to BookExample
        public static BookExample ToBookExample(this BookExampleCreateDTO dto)
        {
            // returns object type BookExample
            // creates new BookExample object and assigns values from BookExampleCreateDTO object
            // converts BookExampleCreateDTO to BookExample entity for database
            return new BookExample
            {
               BookId = dto.BookId, 
               Status = dto.Status,
                ISBN = dto.ISBN
            };
        }
        // extension method -> maping BookExampleUpdateDTO to BookExample
        public static BookExample ToBookExample(this BookExampleUpdateDTO dto)
        {
            // returns object type BookExample
            // creates new BookExample object and assigns values from BookExampleUpdateDTO object
            // converts BookExampleUpdateDTO to BookExample entity for database
            return new BookExample
            {
                Status = dto.Status
            };
        }
    }
}
