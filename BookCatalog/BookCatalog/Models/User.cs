namespace BookCatalog.Models
{
    // defining User ORM (model)
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

        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // collection of RefreshToken objects represents relationship between 1 User and many RefreshTokens
        // -> one-to-many relationship
        // -> initializes empty RefreshToken List to avoid null reference exception
        // -> using for authentification purposes in JWT authentication
        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();   

    }
}
