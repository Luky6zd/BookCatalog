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
    [Route("api/[controller]")]
    [ApiController]
    public class BookExamplesController : ControllerBase
    {
        private readonly DataContext _context;

        public BookExamplesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        // api action/endoint -> GET/all book examples from book example dto table
        public async Task<ActionResult<IEnumerable<BookExampleDTO>>> GetBookExamples()
        {
            List<BookExample> bookExamples = await _context.BookExamples.Include(b => b.Book)
                .ToListAsync();
                
            return bookExamples.Select(bookEx => bookEx.ToBookExampleDTO()).ToList(); 
        }

        // GET: api/BookExamples/5
        // api action/endpoint -> get 1 book example by id from book example detail dto table
        [HttpGet("{id}")]
        public async Task<ActionResult<BookExampleDetailDTO>> GetBookExample(int id)
        {
            var bookExample = await _context.BookExamples.FirstOrDefaultAsync(ex => ex.BookExampleId == id);

            // if book example doesn't exist
            if (bookExample == null)
            {
                return NotFound("Book not found.");
            }

            // if id from URL request matches id from BookExample
            if (id != bookExample.BookExampleId)
            {
                return BadRequest("Id not valid.");
            }
            
            return bookExample.ToBookExampleDetailDTO();
        }

        // PUT: api/BookExamples/5
        // api action/endpoint -> update book example by id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookExample(int id, BookExampleUpdateDTO bookExampleDTO)
        {
            // if id from URL request matches id from BookExampleUpdateDTO
            if (id != bookExampleDTO.BookExampleId)
            {
                return BadRequest("Id not valid.");
            }

            BookExample? bookExample = await _context.BookExamples
                .Include(ex => ex.Book)
                .FirstOrDefaultAsync(ex => ex.BookExampleId == id);

            // if book example doesn't exist
            if (bookExample == null)
            {
                return NotFound("Book doesn't exist.");
            }

            // updates only existing values match by name
            _context.Entry(bookExample).CurrentValues.SetValues(bookExampleDTO);

            // try to save changes async
            try
            {
                await _context.SaveChangesAsync();
            }
            // handling potencial errors/exceptions
            catch (DbUpdateConcurrencyException)
            {
                // if book example doesn't exist
                if (!BookExampleExists(id))
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

        [HttpPost]
        // api action/endpoint -> create new book example
        public async Task<ActionResult<BookExample>> PostBookExample(BookExampleCreateDTO bookExampleDTO)
        {
            BookExample bookExample = bookExampleDTO.ToBookExample();
            _context.BookExamples.Add(bookExample);

            await _context.SaveChangesAsync();

            // return http response with new created book example
            return CreatedAtAction("GetBookExample", new { id = bookExample.BookExampleId }, bookExample);
        }

        // DELETE: api/BookExamples/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookExample(int id)
        {
            // first or default -> returns first matching book example or null (if none is found)
            var bookExample = await _context.BookExamples.FirstOrDefaultAsync(ex => ex.BookExampleId == id);

            // if book example exists
            if (bookExample == null)
            {
                return NotFound("Book not found.");
            }

            // if id from URL request matches id from BookExample
            if (id != bookExample.BookExampleId)
            {
                return BadRequest("Id not valid");
            }

            _context.BookExamples.Remove(bookExample);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExampleExists(int id)
        {
            // check if any book example with given id exists in database
            return _context.BookExamples.Any(e => e.BookExampleId == id);
        }
    }
}
