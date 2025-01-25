using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookCatalog;
using BookCatalog.Models;
using BookCatalog.DTOs_BookExample;
using BookCatalog.Mappers;

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

        // GET: api/BookExamples
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookExampleDTO>>> GetBookExamples()
        {
            List<BookExample> bookExamples = await _context.BookExamples.ToListAsync();
            return bookExamples.Select(bookEx => bookEx.ToBookExampleDTO()).ToList(); 
        }

        // GET: api/BookExamples/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookExampleDetailDTO>> GetBookExample(int id)
        {
            var bookExample = await _context.BookExamples.FindAsync(id);

            if (bookExample == null)
            {
                return NotFound();
            }

            return bookExample.ToBookExampleDetailDTO();
        }

        // PUT: api/BookExamples/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookExample(int id, BookExampleUpdateDTO bookExampleDTO)
        {
            if (id != bookExampleDTO.BookExampleId)
            {
                return BadRequest();
            }

            BookExample? bookExample = await _context.BookExamples.FindAsync(id);

            if (bookExample == null)
            {
                return NotFound();
            }

            _context.Entry(bookExample).CurrentValues.SetValues(bookExampleDTO);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExampleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BookExamples
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookExample>> PostBookExample(BookExampleCreateDTO bookExampleDTO)
        {
            BookExample bookExample = bookExampleDTO.ToBookExample();
            _context.BookExamples.Add(bookExample);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookExample", new { id = bookExample.BookExampleId }, bookExample);
        }

        // DELETE: api/BookExamples/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookExample(int id)
        {
            var bookExample = await _context.BookExamples.FindAsync(id);

            if (bookExample == null)
            {
                return NotFound();
            }

            _context.BookExamples.Remove(bookExample);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExampleExists(int id)
        {
            return _context.BookExamples.Any(e => e.BookExampleId == id);
        }
    }
}
