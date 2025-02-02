using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DTOs.DTOs_Author
{
    // author create (Data Transfer Object)
    public class AuthorCreateDTO
    {
        // properties with data annotations
        [Required]
        [StringLength(20)]
        public string Name { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        public string Title { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        // list of book ids
        [Key]
        public List<int> BookIds { get; set; } = new List<int>();
    }
}
