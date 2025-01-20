using BookCatalog.DTO_s;
using BookCatalog.Models;

namespace BookCatalog.Mappers
{
    public class BookMapper
    {
        public static BookDetailDTO MapBookToBookDetailDTO(Book book)
        {
            return new BookDetailDTO
            {
                BookDetailId = book.BookId,
                //Name = book.Name,
                Title = book.Title
            };
        }

        public static Book MapBookCreateDTOToBook(BookCreateDTO bookCreateDTO)
        {
            return new Book
            {
                //Name = bookCreateDTO.Name,
                Title = bookCreateDTO.Title,
                Genre = bookCreateDTO.Genre
            };
        }

        public static Book MapBookUpdateDTOToBook(BookUpdateDTO bookUpdateDTO)
        {
            return new Book
            {
                BookId = bookUpdateDTO.BookId,
                //Name = bookUpdateDTO.Name,
                Title = bookUpdateDTO.Title,
                Genre = bookUpdateDTO.Genre
            };
        }

        public static Book MapBookExampleToBook(BookExample bookExample)
        {
            return new Book
            {
                BookId = bookExample.BookId,
                //Name = bookExample.Name,
                Title = bookExample.Book?.Title,
                Genre = bookExample.Book?.Genre
            };
        }

    }
}
