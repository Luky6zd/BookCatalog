namespace BookCatalog.DTOs_User
{
    public class UserDTO
    {
        public int UserDTOId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;

    }
}
