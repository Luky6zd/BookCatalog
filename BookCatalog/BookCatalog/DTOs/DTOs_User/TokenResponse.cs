using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DTOs.DTOs_User
{
    public class TokenResponse
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
