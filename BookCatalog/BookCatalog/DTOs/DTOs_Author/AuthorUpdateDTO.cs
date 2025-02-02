using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DTOs.DTOs_Author
{
    // author update (Data Transfer Object)
    public class AuthorUpdateDTO
    {
        // properties
        [Key]
        public int AuthorUpdateDTOId { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Phone]
        public int PhoneNumber { get; set; }
    }
}
