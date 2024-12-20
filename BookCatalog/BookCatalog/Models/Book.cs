namespace BookCatalog.Models
{
    // defining book model
    public class Book
    {
        public int Id { get; set; }
        public string Author { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Year { get; set; }
        public int NumberOfBooks { get; set; }
        public string Publisher { get; set; } = string.Empty;
        public string Press { get; set; } = string.Empty;

    }
}
