using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DTO_s
{
    public class LoginDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
