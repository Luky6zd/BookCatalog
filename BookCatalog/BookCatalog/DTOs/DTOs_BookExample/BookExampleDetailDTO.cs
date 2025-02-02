using BookCatalog.Models;

namespace BookCatalog.DTOs.DTOs_BookExample
{
    // book example detail (Data Transfer Object)
    public class BookExampleDetailDTO
    {
        // properties
        public string Author { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public int Year { get; set; }

        // navigation property -> foreign key on Author
        public Author Authors { get; set; } = null!;
    }
}
