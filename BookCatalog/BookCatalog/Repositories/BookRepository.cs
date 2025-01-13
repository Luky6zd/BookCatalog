
using BookCatalog.Models;
using BookCatalog.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _dataContext;

        public BookRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddBookAsync(Book book)
        {
            await _dataContext.Books.AddAsync(book);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int bookId)
        {
            var book = await _dataContext.Books.FindAsync(bookId);

            if (book != null)
            {
                _dataContext.Books.Remove(book);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _dataContext.Books.ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            return await _dataContext.Books.FindAsync(bookId);
        }

        public async Task UpdateBookAsync(Book book)
        {
            _dataContext.Books.Update(book);
            await _dataContext.SaveChangesAsync();
        }
    }
}
