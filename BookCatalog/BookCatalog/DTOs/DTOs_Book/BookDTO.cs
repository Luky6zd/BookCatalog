
using BookCatalog.DTOs.DTOs_Author;
using BookCatalog.Models;
using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DTOs.DTOs_Book
{
    // book (Data Transfer Object)
    public class BookDTO
    {
        // properties
        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        
        public string Genre { get; set; } = null!;
        
        public string Status { get; set; } = null!;

        public List<AuthorDTO> Authors = new List<AuthorDTO>();
    }
}
