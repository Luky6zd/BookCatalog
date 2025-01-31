using BookCatalog.Models;
using BookCatalog.DTOs.DTOs_User;

namespace BookCatalog.Mappers
{
    public static class UserMapper
    {
        // extension method -> maping User object to UserDTO
        // keyword this means -> this method is going to be called on any User object, as part of User's class
        public static UserDTO ToUserDTO(this User user)
        {
            // returns object of type UserDTO
            // creates new UserDTO object and assigns values from User object
            // mapping values from User to UserDTO
            return new UserDTO
            {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password
            };
        }

        // extension method -> maping User object to UserDetailDTO
        public static UserDetailDTO ToUserDetailDTO(this User user)
        {
            // returns object of type UserDetailDTO
            // creates new UserDetailDTO object and assigns values from User object
            // mapping values from User to UserDetailDTO
            return new UserDetailDTO
            {
                UserDetailDTOId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password
            };
        }

        // extension method -> maping UserCreateDTO to User object
        public static User ToUser(this UserCreateDTO dto)
        {
            // returns object of type User
            // creates new User object and assigns values from UserCreateDTO object
            // mapping values from UserCreateDTO to User
            return new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password
            };
        }

        // extension method -> maping UserUpdateDTO to User object
        public static User ToUser(this UserUpdateDTO dto)
        {
            // returns object of type User
            // creates new User object and assigns values from UserUpdateDTO object
            // mapping values from UserUpdateDTO to User
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
