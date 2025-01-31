using System.ComponentModel.DataAnnotations;


namespace BookCatalog.DTOs.DTOs_Book
{
    public class BookCreateDTO
    {

        [Required]
        [StringLength(20)]
        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        [Required]
        [StringLength(15)]
        public string Genre { get; set; } = null!;

        public List<int> AuthorIds { get; set; } = new List<int>();

        internal static object ToBook(BookCreateDTO bookDTO)
        {
            throw new NotImplementedException();
        }
    }
}
