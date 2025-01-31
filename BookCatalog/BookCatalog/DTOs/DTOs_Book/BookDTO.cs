
using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DTOs.DTOs_Book
{
    public class BookDTO
    {
        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;
        
        public string Genre { get; set; } = null!;
        
        public string Status { get; set; } = null!;
    }
}
