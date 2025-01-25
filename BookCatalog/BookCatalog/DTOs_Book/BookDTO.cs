using BookCatalog.DTOs_Author;
using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DTOs_Book
{
    public class BookDTO
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public AuthorDTO? Author { get; set; }

        [StringLength(15)]
        public string Genre { get; set; } = string.Empty;

        [StringLength(10)]
        public string Status { get; set; } = null!;
    }
}
