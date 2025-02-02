namespace BookCatalog.Models
{
    // defining ORM Author (model)
    public class Author
    {
        // properties
        public int AuthorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int PhoneNumber { get; set; }
        public int YearOfBirth { get; set; }
        public string Country { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // navigation property -> relationship between 2 entities
        // -> collection of Book objects -> relationship between 1 Author and many Books
        public ICollection<Book> Books { get; set; } = new List<Book>();

    }
}
