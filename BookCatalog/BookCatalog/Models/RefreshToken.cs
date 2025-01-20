namespace BookCatalog.Models
{
    public class RefreshToken
    {
        public int RefreshTokenId { get; set; }
        public required string Value { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        
    }
}
