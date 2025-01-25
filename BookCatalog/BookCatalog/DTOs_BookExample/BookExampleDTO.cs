using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DTOs_BookExample
{
    public class BookExampleDTO
    {
       
        [Required]
        public string Title { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public string ISBN { get; set; } = null!;

        public string Status { get; set; } = null!;
    }
}
