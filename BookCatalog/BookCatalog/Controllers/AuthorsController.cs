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
    // data annotation/atribute for API route -> API endpoint
    [Route("api/[controller]")]
    // validaton of api http requests
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        // readonly link/reference to database
        // readonly means that reference can only be assigned once -> in constructor
        private readonly DataContext _context;

        // constructor
        public AuthorsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Authors//all
        [HttpGet]
        // api action/endoint -> get all authors from author dto table
        // async method -> it can perform other operations while waiting for the operation to complete,
        // doesn't block execution thread while waiting for database query
        // returs ActionResult -> different http response status codes
        // returns IEnumerable<AuthorDTO> -> collection of AuthorDTO objects
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors()
        {
            // async method fetch all authors from database (using EF)
            List<Author> authors = await _context.Authors.ToListAsync();
            // mapping authors to DTOs -> returns list (collection) of IEnumerable AuthorDTO as http response
            return authors.Select(author => author.ToAuthorDTO()).ToList();
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        // api action/endpoint -> get 1 author by id from author detail dto table
        public async Task<ActionResult<AuthorDetailDTO>> GetAuthor(int id)
        {
            // async retrieve single author by primary key/id from database
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.AuthorId == id);

            // if author doesn't exist return status 404
            if (author == null)
            {
                return NotFound("Author not found.");
            }

            // check if id from URL request matches id from Author object
            if (id != author.AuthorId)
            {
                return BadRequest("Id not valid.");
            }
            // return author as AuthorDetailDTO
            return author.ToAuthorDetailDTO();
        }

        // PUT: api/Authors/5
        // api action/endpoint -> update author by id
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        // handles HTTP PUT request for updating author
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDTO authorDTO)
        {
            // check if id from URL request matches id from AuthorUpdateDTO
            if (id != authorDTO.AuthorUpdateDTOId)
            {
                return BadRequest("Id not valid.");
            }

            // async query that searches for author by id in database
            // await -> database query runs async -> doesn't block execution of other operations
            // first or default -> returns first matching author or null (if none is found) 
            Author? author = await _context.Authors.FirstOrDefaultAsync(a => a.AuthorId == id);

            // if author doesn't exist return status 404
            if (author == null)
            {
                return NotFound();
            }

            // update author table with new values from AuthorUpdateDTO
            // maps all matching properties from AuthorUpdateDTO to Author - only updates existing values match by name
            _context.Entry(author).CurrentValues.SetValues(authorDTO);

            // try to save changes to database async 
            try
            {
                await _context.SaveChangesAsync();
            }
            // while handling potencial errors/exceptions
            catch
            {
                // check if author exists in database, return status 404
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
            // returns IActionResult -> http response with status 204
            return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // api action/endpoint -> create new author
        // attribute handles HTTP POST request for creating new author
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(AuthorCreateDTO authorDTO)
        {
            //var author = new Author { Name = authorDTO.Name };
            // converts AuthorCreateDTO to Author, then author is added to database
            Author author = authorDTO.ToAuthor();
            // add author to database
            _context.Authors.Add(author);
            // save changes to database
            await _context.SaveChangesAsync();

            // return 201 CreatedAtAction -> http response with new created author object
            return CreatedAtAction("GetAuthor", new { id = author.AuthorId }, author);
        }

        // api action/endpoint -> delete author by id
        // attribute handles HTTP DELETE request for deleting author
        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            // async query searching author by id in database
            // await -> database query runs async -> doesn't block execution of other operations
            // returns first matching author or null (if none is found)
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.AuthorId == id);

            // check if author exists in database 
            if (author == null)
            {
                return NotFound("Author not found");
            }

            // check if id from URL request matches id from Author
            if (id != author.AuthorId)
            {
                return BadRequest("Id not valid");
            }

            // marks author for deleting
            _context.Authors.Remove(author);
            // permanently deletes author from database
            await _context.SaveChangesAsync();

            // returns IActionResult -> http response with status 204
            return NoContent();
        }

        // check if author exists in database -> search by AuthorId that matches given id
        private bool AuthorExists(int id)
        {
            // returns true if author exists in database
            return _context.Authors.Any(a => a.AuthorId == id);
        }
    }
}
