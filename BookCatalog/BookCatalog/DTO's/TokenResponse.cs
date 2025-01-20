using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DTO_s
{
    public class TokenResponse
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
