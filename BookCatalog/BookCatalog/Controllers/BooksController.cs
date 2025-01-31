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
    // book controller
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // readonly link/reference to database
        // readonly means that reference can only be assigned once -> in constructor
        private readonly DataContext _context;

        // constructor
        public BooksController(DataContext context)
        {
            _context = context;
        }

        // get all books
        // GET: api/Books
        // api action/endoint -> get all books from book dto table
        // async method -> it can perform other operations while waiting for the operation to complete,
        // doesn't block execution thread while waiting for database query
        // returs ActionResult -> different http response status codes
        // returns IEnumerable<BookDTO> -> collection of BookDTO objects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            // async method fetch all books from database (using EF)
            List<Book> books = await _context.Books.ToListAsync();
            // mapping books to DTOs -> returns list (collection) of IEnumerable BookDTO as http response
            return books.Select(book => book.ToBookDTO()).ToList();
        }

        // get 1 book by id
        // api action/endpoint -> get 1 book by id from book detail dto table
        // data annotation/atribute for HTTP GET request
        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailDTO>> GetBook(int id)
        {
            // async retrieve single book by primary key/id from database
            var book = await _context.Books.Include(b => b.Authors).FirstOrDefaultAsync(b => b.BookId == id);

            // if book doesn't exist return status code 404
            if (book == null)
            {
                return NotFound("Book not found.");
            }

            // check if id from URL request matches id from Book object
            if (id != book.BookId)
            {
                return BadRequest("Id not valid.");
            }
            // return book as BookDetailDTO
            return book.ToBookDetailDTO();
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // api action/endpoint -> update book by id
        // data annotation/atribute for HTTP PUT request -> update book
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDTO bookDTO)
        {
            // check if id from URL request matches id from BookUpdateDTO
            if (id != bookDTO.BookUpdateDTOId)
            {
                return BadRequest("Id not valid");
            }

            // async query that searches for book by id in database
            // await -> database query runs async -> doesn't block execution of other operations
            // first or default -> returns first matching book or null (if none is found)
            Book? book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == id);

            // if book doesn't exist return status code 404
            if (book == null)
            {
                return NotFound("Book not found.");
            }

            // update book values with values from BookUpdateDTO
            _context.Entry(book).CurrentValues.SetValues(bookDTO);

            // try to save changes to database async
            try
            {
                // await -> database query runs async -> doesn't block execution of other operations
                await _context.SaveChangesAsync();
            }
            // while handling potencial errors/exceptions
            catch (DbUpdateConcurrencyException)
            {
                // check if book exists in database, return status code 404
                if (!BookExists(id))
                {
                    return NotFound("Book not found.");
                }
                // if book exists in database, throw exception
                else
                {
                    throw;
                }
            }

            // return IActionResult -> http response status code 204
            return NoContent();
        }

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(BookCreateDTO bookDTO)
        {
            Book book = bookDTO.ToBook();

            var authors = await _context.Authors.Where(a => bookDTO.AuthorIds.Contains(a.AuthorId)).ToListAsync();
            book.Authors = authors;
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.BookId }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null)
            {
                return NotFound("Book not found.");
            }

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
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}
