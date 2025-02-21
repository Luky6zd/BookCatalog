using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookCatalog;
using BookCatalog.Models;
using BookCatalog.Mappers;
using BookCatalog.DTOs.DTOs_Book;

namespace BookCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly DataContext _context;

        public BooksController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        // api action/endoint -> GET/all books from book dto table
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            List<Book> books = await _context.Books.Include(a => a.Authors).ToListAsync();

            // if book doesn't exist
            if (books == null)
            {
                return new List<BookDTO>();
            }

            return books.Select(book => book.ToBookDTO()).ToList();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        // api action/endpoint -> GET 1 book by id from book detail dto table
        public async Task<ActionResult<BookDetailDTO>> GetBook(int id)
        {
            var book = await _context.Books.Include(b => b.Authors).FirstOrDefaultAsync(b => b.BookId == id);

            // if book doesn't exist
            if (book == null)
            {
                return NotFound("Book not found.");
            }

            // if id from URL request matches id from Book object
            if (id != book.BookId)
            {
                return BadRequest("Id not valid.");
            }

            return book.ToBookDetailDTO();
        }

        // PUT: api/Books/5
        // api action/endpoint -> update book by id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDTO bookDTO)
        {
            // if id from URL request matches id from BookUpdateDTO
            if (id != bookDTO.BookUpdateDTOId)
            {
                return BadRequest("Id not valid");
            }

            Book? book = await _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Status)
                .FirstOrDefaultAsync(b => b.BookId == id);
            //Book? book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == id);

            // if book doesn't exist
            if (book == null)
            {
                return NotFound("Book not found.");
            }

            _context.Entry(book).CurrentValues.SetValues(bookDTO);

            // try to save changes async
            try
            {
                await _context.SaveChangesAsync();
            }
            // handling potencial errors/exceptions
            catch (DbUpdateConcurrencyException)
            {
                // if book doesn't exist
                if (!BookExists(id))
                {
                    return NotFound("Book not found.");
                }
                // if exists but other error occured, throw exception
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        [HttpPost]
        // api action/endpoint -> create new book
        public async Task<ActionResult<Book>> PostBook(BookCreateDTO bookDTO)
        {
            
            Book book = bookDTO.ToBook();
            
            var authors = await _context.Authors
                .Where(a => bookDTO.AuthorIds.Contains(a.AuthorId))
                .ToListAsync();
            
            book.Authors = authors;
            _context.Books.Add(book);
        
            await _context.SaveChangesAsync();

            // return http response with new book
            return CreatedAtAction("GetBook", new { id = book.BookId }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            // returns first matching book or null (if none is found)
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == id);

            // if book exists
            if (book == null)
            {
                return NotFound("Book not found.");
            }

            // if id from URL request matches id from Book object
            if (id != book.BookId)
            {
                return BadRequest("Id not valid");
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            // returns true if book exists in database
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}
