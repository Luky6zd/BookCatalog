using BookCatalog.Models;

namespace BookCatalog.DTOs_BookExample
{
    public class BookExampleDetailDTO
    {
        public Author Authors { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public int Year { get; set; }
    }
}
