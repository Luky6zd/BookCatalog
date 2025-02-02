namespace BookCatalog.DTOs.DTOs_User
{
    // user (Data Transfer Object)
    public class UserDTO
    {
        public int UserDTOId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;

    }
}
