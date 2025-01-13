using BookCatalog.Models;

namespace BookCatalog.Models
{
    // defining copy of book model
    public class BookExample
    {
        public int BookExampleId { get; set; }
        public int BookId { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public Book? Book { get; set; }
    }
}

