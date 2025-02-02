using BookCatalog.Models;
using BookCatalog.DTOs.DTOs_User;

namespace BookCatalog.Mappers
{
    public static class UserMapper
    {
        // extension method -> maping User object to UserDTO
        // keyword this -> this method is going to be called on any User object, as part of User's class
        public static UserDTO ToUserDTO(this User user)
        {
            // returns object type UserDTO
            // creates new UserDTO object and assigns values from User object
            // converts User entity to UserDTO for api response
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
            // returns object type UserDetailDTO
            // creates new UserDetailDTO object and assigns values from User object
            // converts User entity to UserDetailDTO for api response
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
            // returns object type User
            // creates new User object and assigns values from UserCreateDTO object
            // converts UserCreateDTO to User entity for database
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
            // returns object type User
            // creates new User object and assigns values from UserUpdateDTO object
            // converts UserUpdateDTO to User entity for database
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
