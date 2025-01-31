using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DTOs.DTOs_Author
{
    public class AuthorDTO
    {
        public int AuthorDTOId { get; set; }
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

    }
}
