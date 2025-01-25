using BookCatalog.Models;
using BookCatalog.DTOs_User;
using BookCatalog.DTO_s;

namespace BookCatalog.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToUserDTO(this User user)
        {
            return new UserDTO
            {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password
            };
        }

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

        public static User ToUser(this UserCreateDTO dto)
        {
            return new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password
            };
        }

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
