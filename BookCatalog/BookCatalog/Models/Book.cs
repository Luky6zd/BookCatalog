using BookCatalog.Models;

namespace BookCatalog.Models
{
    // defining ORM Book (model)
    public class Book
    {
        // properties
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Publisher { get; set; } = string.Empty;
        public string Press { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int ISBN { get; set; }

        // initializes empty Author list to avoid null reference exception
        // -> relationship between 1 Book and many Authors
        public ICollection<Author> Authors { get; set; } = new List<Author>();

        // relationship between 1 Author and many BookExamples
        public ICollection<BookExample> BookExamples { get; set; } = new List<BookExample>();

  
    }
}


