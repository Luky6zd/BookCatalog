using BookCatalog.Models;

namespace BookCatalog.DTOs_BookExample
{
    public class BookExampleDetailDTO
    {
        public Author Author { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public int Year { get; set; }
    }
}
