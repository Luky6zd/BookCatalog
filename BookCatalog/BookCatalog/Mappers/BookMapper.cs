using BookCatalog.DTOs.DTOs_Book;
using BookCatalog.Models;

namespace BookCatalog.Mappers
{
    public static class BookMapper
    {
        // extension method -> maping Book entity to BookDTO
        // keyword this -> this method is going to be called on any Book object as part of Book's class
        public static BookDTO ToBookDTO(this Book book)
        {
            // returns object of type BookDTO
            // creates new BookDTO object and assigns values from Book object
            // converts Book entity to BookDTO for api response
            return new BookDTO
            {
                Title = book.Title,
                Genre = book.Genre,
                Status = book.Status
            };
        }

        // extension method -> maping Book object to BookDetailDTO
        public static BookDetailDTO ToBookDetailDTO(this Book book)
        {
            // returns object type BookDetailDTO
            // creates new BookDetailDTO object and assigns values from Book object
            // converts Book entity to BookDetailDTO for api response
            return new BookDetailDTO
            {
                Title = book.Title,
                Genre = book.Genre,
                Status = book.Status,
                Year = book.Year,
                Authors = book.Authors.Select(author => author.ToAuthorDTO()).ToList()
            };
        }

        // extension method -> maping BookCreateDTO to Book object
        public static Book ToBook(this BookCreateDTO dto)
        {
            // returns object type Book
            // creates new Book object and assigns values from BookCreateDTO object
            // converts BookCreateDTO to Book entity for database
            return new Book
            {
                Title = dto.Title,
                Genre = dto.Genre,
                //Authors = dto.Author.Select(author => author.ToAuthor()).ToList()
            };
        }

        // extension method maping BookUpdateDTO to Book object
        public static Book ToBook(this BookUpdateDTO dto)
        {
            // returns object type Book
            // creates new Book object and assigns values from BookUpdateDTO object
            // converts BookUpdateDTO to Book entity for database
            return new Book
            {
                Title = dto.Title,
                Genre = dto.Genre,
                Status = dto.Status
            };
        }
    }
}
