using BookCatalog.Models;
using BookCatalog.DTOs.DTOs_User;

namespace BookCatalog.Mappers
{
    public static class UserMapper
    {
        // extension method converts User to UserDTO for api response
        public static UserDTO ToUserDTO(this User user)
        {
            return new UserDTO
            {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password
            };
        }

        // extension method converts User to UserDetailDTO for api response
        public static UserDetailDTO ToUserDetailDTO(this User user)
        {
            return new UserDetailDTO
            {
                UserDetailDTOId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password
            };
        }

        // extension method converts UserCreateDTO to User entity for database
        public static User ToUser(this UserCreateDTO dto)
        {
            return new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password
            };
        }

        // extension method converts UserUpdateDTO to User entity for database
        public static User ToUser(this UserUpdateDTO dto)
        {
            return new User
            {
                UserId = dto.UserUpdateDTOId,
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password
            };
        }
    }
}
