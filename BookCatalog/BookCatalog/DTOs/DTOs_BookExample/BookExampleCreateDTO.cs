namespace BookCatalog.DTOs.DTOs_BookExample
{
    // book example create (Data Transfer Object)
    public class BookExampleCreateDTO
    {
        // properties
        public int BookId { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
