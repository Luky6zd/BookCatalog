using BookCatalog.DTOs_Author;
using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DTOs_Book
{
    public class BookDTO
    {
        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Author { get; set; } = null!;

        [StringLength(15)]
        public string Genre { get; set; } = null!;

        [StringLength(10)]
        public string Status { get; set; } = null!;
    }
}
