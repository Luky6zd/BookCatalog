namespace BookCatalog.Models
{
    // defining author model
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int PhoneNumber { get; set; }
        public int YearOfBirth { get; set; }
        public string Country { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public ICollection<Book> Books { get; set; } = new List<Book>();

    }
}
