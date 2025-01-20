using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DTO_s
{
    public class UserCreateDTO
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(15)]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(8)]
        public string Password { get; set; } = null!;
    }
}
