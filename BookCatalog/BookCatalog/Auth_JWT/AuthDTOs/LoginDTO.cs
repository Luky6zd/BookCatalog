using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Auth_JWT.AuthDTOs
{
    // user login (Data Transfer Object)
    public class LoginDTO
    {
        // properties
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
