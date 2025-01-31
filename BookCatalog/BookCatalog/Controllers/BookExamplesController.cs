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
using BookCatalog.DTOs.DTOs_BookExample;

namespace BookCatalog.Controllers
{
    // data annotation/atribute for API route -> API endpoint
    // validaton of api http requests
    [Route("api/[controller]")]
    [ApiController]
    public class BookExamplesController : ControllerBase
    {
        // readonly link/reference to database
        // readonly means that reference can only be assigned once -> in constructor
        private readonly DataContext _context;

        // constructor
        public BookExamplesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/BookExamples-all
        [HttpGet]
        // api action/endoint -> get all book examples from book example dto table
        // async method -> it can perform other operations while waiting for the operation to complete,
        // doesn't block execution thread while waiting for database query
        // returs ActionResult -> different http response status codes
        // returns IEnumerable<BookExampleDTO> -> collection of BookExampleDTO objects
        public async Task<ActionResult<IEnumerable<BookExampleDTO>>> GetBookExamples()
        {
            // async method fetch all book examples from database (using EF)
            List<BookExample> bookExamples = await _context.BookExamples.ToListAsync();
            // mapping book examples to DTOs -> returns list (collection) of IEnumerable BookExampleDTO as
            // http response
            return bookExamples.Select(bookEx => bookEx.ToBookExampleDTO()).ToList(); 
        }

        // GET: api/BookExamples/5
        // api action/endpoint -> get 1 book example by id from book example detail dto table
        [HttpGet("{id}")]
        public async Task<ActionResult<BookExampleDetailDTO>> GetBookExample(int id)
        {
            // async retrieve single book example by primary key/id from database
            var bookExample = await _context.BookExamples.FirstOrDefaultAsync(ex => ex.BookExampleId == id);

            // if book example doesn't exist return status code 404
            if (bookExample == null)
            {
                return NotFound("Book not found.");
            }

            // check if id from URL request matches id from BookExample object
            if (id != bookExample.BookExampleId)
            {
                return BadRequest("Id not valid.");
            }
            // return book example as BookExampleDetailDTO
            return bookExample.ToBookExampleDetailDTO();
        }

        // PUT: api/BookExamples/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // api action/endpoint -> update book example by id
        // data annotation/atribute for HTTP PUT request -> update book example
        [HttpPut("{id}")]
        // handles HTTP PUT request for updating book example
        public async Task<IActionResult> PutBookExample(int id, BookExampleUpdateDTO bookExampleDTO)
        {
            // check if id from URL request matches id from BookExampleUpdateDTO
            if (id != bookExampleDTO.BookExampleId)
            {
                return BadRequest("Id not valid.");
            }

            // async query searching book example by id in database
            // await -> database query runs async -> doesn't block execution of other operations
            // first or default -> returns first matching book example or null (if none is found)
            BookExample? bookExample = await _context.BookExamples.FirstOrDefaultAsync(ex => ex.BookExampleId ==id);

            // if book example doesn't exist return status code 404
            if (bookExample == null)
            {
                return NotFound("Book doesn't exist.");
            }

            // update book example values with values from BookExampleUpdateDTO
            // maps all matching properties from BookExampleUpdateDTO to BookExample - only updates existing values match by name
            _context.Entry(bookExample).CurrentValues.SetValues(bookExampleDTO);

            // try to save changes to database
            try
            {
                // async save changes to database
                await _context.SaveChangesAsync();
            }
            // while handling potencial errors/exceptions
            catch (DbUpdateConcurrencyException)
            {
                // check if book example exists in database, return status code 404
                if (!BookExampleExists(id))
                {
                    return NotFound("Book not found.");
                }
                // if book example exists but other error occured, throw exception
                else
                {
                    throw;
                }
            }

            // return IActionResult -> http response with status 204
            return NoContent();
        }

        // POST: api/BookExamples
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        // api action/endpoint -> create new book example
        // attribute handles HTTP POST request for creating new book example
        public async Task<ActionResult<BookExample>> PostBookExample(BookExampleCreateDTO bookExampleDTO)
        {
            // convert BookExampleCreateDTO to BookExample, using extension method
            BookExample bookExample = bookExampleDTO.ToBookExample();
            // add new book example to database
            _context.BookExamples.Add(bookExample);
            // save changes to database
            await _context.SaveChangesAsync();

            // return CreatedAtAction -> http response with new created book example object -> status code 201
            return CreatedAtAction("GetBookExample", new { id = bookExample.BookExampleId }, bookExample);
        }

        // DELETE: api/BookExamples/5
        // api action/endpoint -> delete book example by id
        // attribute handles HTTP DELETE request for deleting book example
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookExample(int id)
        {
            // async query searching book example by id in database
            // await -> database query runs async -> doesn't block execution of other operations
            // first or default -> returns first matching book example or null (if none is found)
            var bookExample = await _context.BookExamples.FirstOrDefaultAsync(ex => ex.BookExampleId == id);

            // check if book example exists in database
            if (bookExample == null)
            {
                return NotFound("Book not found.");
            }

            // check if id from URL request matches id from BookExample object
            if (id != bookExample.BookExampleId)
            {
                return BadRequest("Id not valid");
            }

            // marks book example for deleting
            _context.BookExamples.Remove(bookExample);
            // permanently delete book example from database
            await _context.SaveChangesAsync();

            // return IActionResult -> http response with status code 204
            return NoContent();
        }

        // check if book example exists in database -> search by BookExampleId that matches given id
        private bool BookExampleExists(int id)
        {
            // LINQ query -> check if any book example with given id exists in database
            // returns true if book example exists
            return _context.BookExamples.Any(e => e.BookExampleId == id);
        }
    }
}
