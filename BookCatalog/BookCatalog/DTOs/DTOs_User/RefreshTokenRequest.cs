using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DTOs.DTOs_User
{
    public class RefreshTokenRequest
    {
        [Required]
        public required string RefreshToken { get; set; } = null!;
    }
}
