using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Auth_JWT.AuthDTOs
{
    // token response
    public class TokenResponse
    {
        // properties
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
