using BookCatalog.DTOs.DTOs_BookExample;
using BookCatalog.Models;

namespace BookCatalog.Mappers
{
    public static class BookExampleMapper
    {
        // extension method -> maping BookExample to BookExampleDTO
        // keyword this means -> this method is going to be called on any BookExample object, as part of BookExample's class
        public static BookExampleDTO ToBookExampleDTO(this BookExample bookExample)
        {
            // returns object of type BookExampleDTO
            // creates new BookExampleDTO object and assigns values from BookExample object
            // mapping values from BookExample to BookExampleDTO
            return new BookExampleDTO
            {
                BookExampleId = bookExample.BookExampleId,
                Status = bookExample.Status
            };
        }

        // extension method -> maping BookExample to BookExampleDetailDTO
        public static BookExampleDetailDTO ToBookExampleDetailDTO(this BookExample bookExample)
        {
            // returns object of type BookExampleDetailDTO
            // creates new BookExampleDetailDTO object and assigns values from BookExample object
            // mapping values from BookExample to BookExampleDetailDTO
            return new BookExampleDetailDTO
            {
                
            };
        }

        // extension method -> maping BookExampleCreateDTO to BookExample
        public static BookExample ToBookExample(this BookExampleCreateDTO dto)
        {
            // returns object of type BookExample
            // creates new BookExample object and assigns values from BookExampleCreateDTO object
            // mapping values from BookExampleCreateDTO to BookExample
            return new BookExample
            {
               
            };
        }
        // extension method -> maping BookExampleUpdateDTO to BookExample
        public static BookExample ToBookExample(this BookExampleUpdateDTO dto)
        {
            // returns object of type BookExample
            // creates new BookExample object and assigns values from BookExampleUpdateDTO object
            // mapping values from BookExampleUpdateDTO to BookExample
            return new BookExample
            {
                Status = dto.Status
            };
        }
    }
}
