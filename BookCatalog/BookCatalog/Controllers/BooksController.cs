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
        // readonly -> reference can only be assigned once -> in constructor
        private readonly DataContext _context;

        // constructor
        public BooksController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Books
        // api action/endoint -> get all books from book dto table
        // async method -> it can perform other operations while waiting for the operation to complete,
        // doesn't block execution thread while waiting for database query
        // returs ActionResult -> different http response status codes
        // returns IEnumerable<BookDTO> -> List of BookDTO objects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            // async method fetch all books from database
            List<Book> books = await _context.Books.ToListAsync();

            // if book doesn't exist
            if (books == null)
            {
                // return empty list
                return new List<BookDTO>();
            }

            // mapping books to DTOs -> return list of BookDTOs as http response
            return books.Select(book => book.ToBookDTO()).ToList();
            //return Ok(books.Select(book => (BookDTO) book.ToBookDTO()).ToList());
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        // api action/endpoint -> get 1 book by id from book detail dto table
        // data annotation/atribute for HTTP GET request
        public async Task<ActionResult<BookDetailDTO>> GetBook(int id)
        {
            // async retrieve 1 book by primary key/id from database
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
            // if id from URL request matches id from BookUpdateDTO
            if (id != bookDTO.BookUpdateDTOId)
            {
                return BadRequest("Id not valid");
            }

            // async query -> search book by id in database
            // await -> database query runs async -> doesn't block execution of other operations
            // first or default -> returns first matching book or null (if none is found)
            Book? book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == id);

            // if book doesn't exist
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

            // return ActionResult -> http response
            return NoContent();
        }

        // POST: api/Books
        // api action/endpoint -> create new book
        // data annotation/attribute handles http post request for creating new book
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(BookCreateDTO bookDTO)
        {
            // extension method -> converts book create DTO(used in api) into Book entity(used ina database)
            Book book = bookDTO.ToBook();
            // fetch list from database  table Authors by author id which exists in bookDTO.AuthorId
            // -> list of author IDs sent from client
            var authors = await _context.Authors
                // query -> filter author table and retrives authors whose AuthorId is in bookDTO.AuthorIds list
                .Where(a => bookDTO.AuthorIds.Contains(a.AuthorId))
                // execute query async and returns list of Authors that match Ids from bookDTO
                .ToListAsync();
            // link authors to book
            book.Authors = authors;
            // add book to database
            _context.Books.Add(book);
            // save changes to database
            await _context.SaveChangesAsync();

            // return ActionResult -> http response with new book
            return CreatedAtAction("GetBook", new { id = book.BookId }, book);
        }

        // DELETE: api/Books/5
        // api action/endpoint -> delete book by id
        // data annotation/attribute handles HTTP DELETE request for deleting book
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            // async query searching book by id in database
            // await -> database query runs async -> doesn't block execution of other operations
            // returns first matching book or null (if none is found)
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == id);

            // if book exists in database
            if (book == null)
            {
                return NotFound("Book not found.");
            }

            // if id from URL request matches id from Book object
            if (id != book.BookId)
            {
                return BadRequest("Id not valid");
            }

            // mark book for deleting
            _context.Books.Remove(book);
            // permanently delete book from database
            await _context.SaveChangesAsync();

            // return ActionResult -> http response
            return NoContent();
        }

        // check if book exists
        private bool BookExists(int id)
        {
            // returns true if book exists in database
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}
