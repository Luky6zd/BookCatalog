using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DTO_s
{
    public class BookDetailDTO
    {
        [Key]
        [Required]
        public int BookDetailId { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; } = null!;

        [Required]
        public string Title { get; set; } = null!;
    }
}
