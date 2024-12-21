
using BookCatalog.Models;

namespace BookCatalog.Repository
{
    public class BookRepository : IBookRepository
    {
        public Task AddBookAsync(Book book)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBookAsync(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetBookByIdAsync(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBookAsync(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
