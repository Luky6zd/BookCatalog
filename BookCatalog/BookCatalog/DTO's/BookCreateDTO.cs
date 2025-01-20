using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DTO_s
{
    public class BookCreateDTO
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        [StringLength(15)]
        public string Name { get; set; } = null!;

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(8)]
        public string Genre { get; set; } = null!;
        
    }
}
