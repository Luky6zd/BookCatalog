using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DTOs.DTOs_Author
{
    // author (Data Transfer Object)
    public class AuthorDTO
    {
        // properties
        [Key]
        public int AuthorDTOId { get; set; }
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

    }
}
