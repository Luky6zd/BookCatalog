using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Auth_JWT.AuthDTOs
{
    // token request
    public class RefreshTokenRequest
    {
        [Required]
        public required string RefreshToken { get; set; } = null!;
    }
}
