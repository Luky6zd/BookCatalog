using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookCatalog;
using BookCatalog.Models;

namespace BookCatalog.Pagination
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookPaginationController : ControllerBase
    {
        private readonly DataContext _context;

        public BookPaginationController(DataContext context)
        {
            _context = context;
        }

        // GET: api/BookPagination
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number must be greater than 0!");
            }

            var totalBooks = await _context.Books.CountAsync();

            var books = await _context.Books
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = new
            {
                TotalCount = totalBooks,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalBooks / (double)pageSize),
                Books = books
            };

            return Ok(response);
        }

    }
}
