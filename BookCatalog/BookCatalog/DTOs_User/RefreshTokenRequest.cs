using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DTO_s
{
    public class RefreshTokenRequest
    {
        [Required]
        public required string RefreshToken { get; set; } = null!;
    }
}
