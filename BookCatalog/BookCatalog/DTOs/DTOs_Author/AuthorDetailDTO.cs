namespace BookCatalog.DTOs.DTOs_Author
{
    // author detail (Data Transfer Object)
    public class AuthorDetailDTO
    {
        // properties
        public int AuthorDetailDTOId { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int? PhoneNumber { get; set; }

    }
}
