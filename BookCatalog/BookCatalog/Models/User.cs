namespace BookCatalog.Models
{
    // defining user model
    // only for authentification purposes
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public int OIB { get; set; }
        public string Genre { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int MembershipNumber { get; set; }
        public int PIN { get; set; }

    }
}
