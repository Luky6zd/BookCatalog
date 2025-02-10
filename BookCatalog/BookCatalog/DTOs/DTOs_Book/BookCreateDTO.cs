using System.ComponentModel.DataAnnotations;


namespace BookCatalog.DTOs.DTOs_Book
{
    // book create (Data Transfer Object)
    public class BookCreateDTO
    {
        // properties
        [Required]
        [StringLength(20)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(15)]
        public string Genre { get; set; } = null!;

        [Key]
        public List<int> AuthorIds { get; set; } = new List<int>();

    }
}
