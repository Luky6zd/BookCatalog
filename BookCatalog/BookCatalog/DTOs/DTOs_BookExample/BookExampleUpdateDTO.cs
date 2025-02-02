using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DTOs.DTOs_BookExample
{
    // book example update (Data Transfer Object)
    public class BookExampleUpdateDTO
    {
        // properties
        [Key]
        public int BookExampleId { get; set; }

        [Required]
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string ISBN { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}
