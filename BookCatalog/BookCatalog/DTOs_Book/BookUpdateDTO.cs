using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DTOs_Book
{
    public class BookUpdateDTO
    {
        [Required]
        public string Title { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public string Status { get; set; } = null!;
    }
}
