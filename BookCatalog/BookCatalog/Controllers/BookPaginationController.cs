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
        public async Task<ActionResult<PageResult<Book>>> GetBooks(
            // extracts values from url query parametrs
            [FromQuery] int pageNumber = 1, 
            [FromQuery] int pageSize = 3)
        {
            // if pageNumber and pageSize are valid
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number must be greater than 0!");
            }

            // AsQueryable converts DBSet<Book> into IQueryable<Book>
            var query = _context.Books.AsQueryable();
            
            var totalBooks = await query.ToPageListAsync(pageNumber, pageSize);

            var totalCount = await query.CountAsync();

            // async pagination books
            var books = await query
                .OrderBy(b => b.Title) 
                .OrderBy(b => b.BookId)
                .Include(b => b.Authors)
                .Include(b => b.Year)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // instantiation of PageResult<Book> object
            var response = new PageResult<Book>(books, totalCount, pageNumber, pageSize)
            {
                // set properties of PageResult<Book> object
                Items = books,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                CurrentPage = pageNumber,
                PageSize = pageSize
            };

            return Ok(response);
        }

    }
}
