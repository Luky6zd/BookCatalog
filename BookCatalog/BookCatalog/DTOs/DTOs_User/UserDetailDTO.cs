using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DTOs.DTOs_User
{
    public class UserDetailDTO
    {
        [Key]
        public int UserDetailDTOId { get; set; }
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
