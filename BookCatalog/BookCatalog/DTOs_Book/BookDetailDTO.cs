using System.ComponentModel.DataAnnotations;
using BookCatalog.DTOs_Author;

namespace BookCatalog.DTOs_Book
{
    public class BookDetailDTO
    {
        [Required]
        [StringLength(20)]
        public string Title { get; set; } = null!;

        [Required]
        public AuthorDTO? Authors { get; set; }
        public string Author { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public string Description { get; set; } = string.Empty;
        
        public int Year { get; set; }

        public string Status { get; set; } = null!;
    }
}
