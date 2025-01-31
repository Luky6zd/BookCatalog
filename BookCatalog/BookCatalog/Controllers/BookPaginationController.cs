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
        // readonly field for interacting with database
        private readonly DataContext _context;

        // constructor for BookPaginationController
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
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 3)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number must be greater than 0!");
            }

            // query for all books -> representing Books table in database
            // AsQueryable converts DBSet<Book> into IQueryable<Book> for LINQ queries
            var query = _context.Books.AsQueryable();
            // calculate total number of book records in database without actually loading data into memory,
            // executes query -> count how many books match the filter and return result
            // await waits for operation to complete without blocking the thread->doesn't block execution of other operations while waiting calculation
            var totalBooks = await query.ToPageListAsync(pageNumber, pageSize); // executes query async

            var totalCount = await query.CountAsync(); // executes query async

            // pagination of books using LINQ
            var books = await query
                // chaining LINQ methods
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(); // executes query async -> fetch current page of books

            // instantiation of PageResult<Book> object -> create pagination response
            var response = new PageResult<Book>(books, totalCount, pageNumber, pageSize)
            {
                // set properties of PageResult<Book> object
                Items = books,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                CurrentPage = pageNumber,
                PageSize = pageSize
            };

            // returns async HTTP response with PageResult<Book> object
            return Ok(response);
        }

    }
}
