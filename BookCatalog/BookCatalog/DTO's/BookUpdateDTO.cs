namespace BookCatalog.DTO_s
{
    public class BookUpdateDTO
    {
        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
    }
}
