using BookCatalog.Models;

namespace BookCatalog.Models
{
    // defining copy of book model
    public class BookExample
    {
        public int BookExampleId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int Year { get; set; }

        public Book? Book { get; set; }
    }
}

