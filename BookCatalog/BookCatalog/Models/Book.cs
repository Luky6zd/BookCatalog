using BookCatalog.Models;

namespace BookCatalog.Models
{
    // defining book model
    public class Book
    {
        // book id for each book
        public int BookId { get; set; }
        public string Author { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Publisher { get; set; } = string.Empty;
        public string Press { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public int ISBN { get; set; }


        // collection of users -> initializes as empty user list
        public ICollection<Author> Authors { get; set; } = new List<Author>();
        public ICollection<BookExample> BookExamples { get; set; } = new List<BookExample>();

    }
}


