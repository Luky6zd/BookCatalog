using BookCatalog.Models;

namespace BookCatalog.Models
{
    // defining ORM copy of book (model)
    public class BookExample
    {
        public int BookExampleId { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        // foreign key on Book -> 1 Book can have many BookExamples
        public int BookId { get; set; }

        // Book? navigation property
        // relationship between many BookExamples and 1 Book
        public Book? Book { get; set; }
    }
}

