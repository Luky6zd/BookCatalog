namespace BookCatalog.DTOs_BookExample
{
    public class BookExampleCreateDTO
    {
        public string Author { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int Year { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
