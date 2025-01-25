namespace BookCatalog.DTOs_Author
{
    public class AuthorDetailDTO
    {
        public int AuthorDetailDTOId { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int? PhoneNumber { get; set; }

    }
}
