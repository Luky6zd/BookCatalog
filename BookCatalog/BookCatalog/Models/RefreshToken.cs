namespace BookCatalog.Models
{
    // defining RefreshToken ORM (model)
    public class RefreshToken
    {
        // properties
        public int RefreshTokenId { get; set; }
        public required string Value { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }

        // foreign key on User
        //public int UserId { get; set; }
        //public User? User { get; set; }

    }
}
