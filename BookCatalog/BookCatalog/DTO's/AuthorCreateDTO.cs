using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DTO_s
{
    public class AuthorCreateDTO
    {
        [Key]
        public int AuthorId { get; set; }
        [Required]
        [StringLength(15)]
        public string Name { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        public string Title { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;
    }
}
