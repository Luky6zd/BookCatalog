using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookCatalog.Models;
using BookCatalog.Pagination;
using BookCatalog.DTOs.DTOs_Book;


namespace BookCatalog.Controllers
{
    // data annotation/atributes for API route -> API endpoint
    [Route("api/[controller]")]
    [ApiController]
    public class BookPaginationController : ControllerBase
    {
        // reference to database
        private readonly DataContext _context;

        // constructor
        public BookPaginationController(DataContext context)
        {
            // context is injected into controller through constructor
            _context = context;
        }

        // GET: api/BookPagination
        // data annotation/atribute for HTTP GET request
        [HttpGet]
        public async Task<ActionResult<PageResult<Book>>> GetBooks(
            // query parameters for controling pagination
            // extracts values from url query parametrs
            [FromQuery] int pageNumber = 1, // default value
            [FromQuery] int pageSize = 3) // default value
        {
            // if pageNumber and pageSize are valid
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number must be greater than 0!");
            }

            // query for all books -> representing Books table in database
            // AsQueryable converts DBSet<Book> into IQueryable<Book> for LINQ queries
            var query = _context.Books.AsQueryable();
            // calculate total number of books in database without actually loading data into memory,
            // executes query -> count how many books match the filter and return result
            // await waits for operation to complete without blocking the thread->doesn't block execution of other operations while waiting calculation
            var totalBooks = await query.ToPageListAsync(pageNumber, pageSize); // executes query async

            var totalCount = await query.CountAsync(); // execute query async

            // async pagination books -> LINQ queries
            var books = await query
                // chaining LINQ queries
                .OrderBy(b => b.Title) // order books by title
                .OrderBy(b => b.BookId) // order books by id
                .Include(b => b.Authors) // include authors for each book
                .Include(b => b.Year) // include year for each book
                .Skip((pageNumber - 1) * pageSize) // skip books based on page
                .Take(pageSize) // only required number of books
                .ToListAsync(); // executes query async -> fetch current page of books

            // instantiation of PageResult<Book> object -> creates paginated response
            var response = new PageResult<Book>(books, totalCount, pageNumber, pageSize)
            {
                // set properties of PageResult<Book> object
                Items = books, // books for requested page
                TotalCount = totalCount, // total number of books in DB
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize), // calculates total number of pages required for all books
                CurrentPage = pageNumber, // current page
                PageSize = pageSize // number of books per page
            };

            // returns async HTTP response with PageResult<Book> object
            return Ok(response);
        }

    }
}
