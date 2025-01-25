using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DTOs_User
{
    public class UserUpdateDTO
    {
        [Key]
        public int UserUpdateDTOId { get; set; }

        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        public int PhoneNumber { get; set; }
    }
}
