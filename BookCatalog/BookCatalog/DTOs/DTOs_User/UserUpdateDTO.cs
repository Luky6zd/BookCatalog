using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DTOs.DTOs_User
{
    // user update (Data Transfer Object)
    public class UserUpdateDTO
    {
        // properties
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
