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
using BookCatalog.DTOs.DTOs_Author;

namespace BookCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly DataContext _context;

        public AuthorsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        // api action/endoint -> get all authors from author dto table
        // returns IEnumerable<AuthorDTO> -> List of AuthorDTO objects
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors()
        {
            List<Author> authors = await _context.Authors.Include(b => b.Books).ToListAsync();
            return authors.Select(author => author.ToAuthorDTO()).ToList();
        }

        [HttpGet("{id}")]
        // api action/endpoint -> get 1 author by id from author detail dto table
        public async Task<ActionResult<AuthorDetailDTO>> GetAuthor(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.AuthorId == id);

            // if author doesn't exist
            if (author == null)
            {
                return NotFound("Author not found.");
            }

            // if id from URL request matches id from Author
            if (id != author.AuthorId)
            {
                return BadRequest("Id not valid.");
            }

            return author.ToAuthorDetailDTO();
        }

        
        [HttpPut("{id}")]
        // api action/endpoint -> update author by id
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDTO authorDTO)
        {
            // if id from URL request matches id from AuthorUpdateDTO
            if (id != authorDTO.AuthorUpdateDTOId)
            {
                return BadRequest("Id not valid.");
            }

            Author? author = await _context.Authors.FirstOrDefaultAsync(a => a.AuthorId == id);

            // if author doesn't exist
            if (author == null)
            {
                return NotFound();
            }

            _context.Entry(author).CurrentValues.SetValues(authorDTO);

            // try to save changes async 
            try
            {
                await _context.SaveChangesAsync();
            }
            // handling potencial errors/exceptions
            catch
            {
                // if author exists in database
                if (!AuthorExists(id))
                {
                    return NotFound();
                }
                // if author exists but other error occured, throw exception
                else
                {
                    throw;
                }
            }
            
            return NoContent();
        }

        [HttpPost]
        // api action/endpoint -> create new author
        public async Task<ActionResult<Author>> PostAuthor(AuthorCreateDTO authorDTO)
        {
            Author author = authorDTO.ToAuthor();

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            // return http response with new created author object
            return CreatedAtAction("GetAuthor", new { id = author.AuthorId }, author);
        }

        
        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.AuthorId == id);

            // if author exists in database 
            if (author == null)
            {
                return NotFound("Author not found");
            }

            // if id from URL request matches id from Author
            if (id != author.AuthorId)
            {
                return BadRequest("Id not valid");
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(int id)
        {
            // returns true if author exists in database
            return _context.Authors.Any(a => a.AuthorId == id);
        }
    }
}
