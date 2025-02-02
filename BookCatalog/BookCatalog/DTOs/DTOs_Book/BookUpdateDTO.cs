using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DTOs.DTOs_Book
{
    // book update (Data Transfer Object)
    public class BookUpdateDTO
    {
        // properties
        [Key]
        public int BookUpdateDTOId { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Author { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public string Status { get; set; } = null!;
    }
}
