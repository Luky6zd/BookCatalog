
using BookCatalog.DTOs.DTOs_Author;
using BookCatalog.Models;

namespace BookCatalog.Mappers
{
    public static class AuthorMapper
    {
        // extension method -> maping Author object (entity) to AuthorDTO
        // keyword this -> this method is going to be called on any Author object, as part of Author's class
        public static AuthorDTO ToAuthorDTO(this Author author)
        {
            // returns object type AuthorDTO
            // creates new AuthorDTO object and assigns values from Author object
            // converts Author entity to AuthorDTO for api response
            return new AuthorDTO
            {
                AuthorDTOId = author.AuthorId, // mapping AuthorId to AuthorDTOId
                Name = author.Name, 
                LastName = author.LastName, 
                Email = author.Email
            };
        }

        // extension method -> maping Author object (entity) to AuthorDetailDTO
        public static AuthorDetailDTO ToAuthorDetailDTO(this Author author)
        {
            // returns object type AuthorDetailDTO
            // creates new AuthorDetailDTO object and assigns values from Author object
            // converts Author entity to AuthorDetailDTO for api response
            return new AuthorDetailDTO
            {
                AuthorDetailDTOId = author.AuthorId, // mapping AuthorId to AuthorDetailDTOId
                Name = author.Name,
                LastName = author.LastName,
                Email = author.Email,
                PhoneNumber = author.PhoneNumber
            };
        }

        // extension method -> maping AuthorCreateDTO to Author object (entity)
        public static Author ToAuthor(this AuthorCreateDTO dto)
        {
            // returns object type Author
            // creates new Author object and assigns values from AuthorCreateDTO object
            // converts AuthorCreateDTO to Author entity for database
            return new Author
            {
                Name = dto.Name, // mapping Name to Name
                LastName = dto.LastName,
                Email = dto.Email
            };
        }

        // extension method -> maping AuthorUpdateDTO to Author object (entity)
        public static Author ToAuthor(this AuthorUpdateDTO dto)
        {
            // returns object type Author
            // creates new Author object and assigns values from AuthorUpdateDTO object
            // converts AuthorUpdateDTO to Author entity for database
            return new Author
            {
                Name = dto.Name, // mapping Name to Name
                LastName = dto.LastName,
                Email = dto.Email
            };
        }

    }
}
