using BookCatalog.Models;

namespace BookCatalog.Models
{
    // defining ORM copy of book (model)
    public class BookExample
    {
        public int BookExampleId { get; set; }
        //public string Title { get; set; } = string.Empty;
        //public string Author { get; set; } = string.Empty;
        //public string Genre { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        //public int Year { get; set; }

        // foreign key on Book
        // 1 Book can have many BookExamples
        public int BookId { get; set; }

        // Book? navigation property
        // represents relationship between many BookExamples and 1 Book
        public Book? Book { get; set; }
    }
}

