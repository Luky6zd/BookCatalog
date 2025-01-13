namespace BookCatalog.Models
{
    // defining book model
    public class Book
    {
        // book id for each book
        public int Id { get; set; }
        public string Author { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Year { get; set; }
        //public int BookCopies { get; set; }
        public string Publisher { get; set; } = string.Empty;
        public string Press { get; set; } = string.Empty;

        // id of library who has/ows book -> establishing relationship between Book and Library in DB
        public int LibraryId { get; set; }

        // id of user who has/ows book -> establishing relationship between Book and User in DB
        public int UserId { get; set; }

        // NAVIGACIJSKO SVOJSTVO-nullable property of type Library
        public Library? Library { get; set; }
        // collection of users -> initializes as empty user list
        public ICollection<User> Users { get; set; } = new List<User>();

    }
}
