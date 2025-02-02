using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DTOs.DTOs_User
{
    // user create (Data Transfer Object)
    public class UserCreateDTO
    {
        // properties
        [Key]
        public int UserCreateDTOId { get; set; }

        [Required]
        [StringLength(15)]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(8)]
        public string Password { get; set; } = null!;

        public int PhoneNumber { get; set; }

    }
}
