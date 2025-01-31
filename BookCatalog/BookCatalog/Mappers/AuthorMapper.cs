
using BookCatalog.DTOs.DTOs_Author;
using BookCatalog.Models;

namespace BookCatalog.Mappers
{
    public static class AuthorMapper
    {
        // extension method -> maping Author object (entity) to AuthorDTO
        // keyword this means -> this method is going to be called on any Author object, as part of Author's class
        public static AuthorDTO ToAuthorDTO(this Author author)
        {
            // returns object of type AuthorDTO
            // creates new AuthorDTO object and assigns values from Author object
            // mapping values from Author to AuthorDTO
            return new AuthorDTO
            {
                AuthorDTOId = author.AuthorId,
                Name = author.Name,
                LastName = author.LastName,
                Email = author.Email
            };
        }

        // extension method -> maping Author object (entity) to AuthorDetailDTO
        public static AuthorDetailDTO ToAuthorDetailDTO(this Author author)
        {
            // returns object of type AuthorDetailDTO
            // creates new AuthorDetailDTO object and assigns values from Author object
            // mapping values from Author to AuthorDetailDTO
            return new AuthorDetailDTO
            {
                AuthorDetailDTOId = author.AuthorId,
                Name = author.Name,
                LastName = author.LastName,
                Email = author.Email,
                PhoneNumber = author.PhoneNumber
            };
        }

        // extension method -> maping AuthorCreateDTO to Author object (entity)
        public static Author ToAuthor(this AuthorCreateDTO dto)
        {
            // returns object of type Author
            // creates new Author object and assigns values from AuthorCreateDTO object
            // mapping values from AuthorCreateDTO to Author
            return new Author
            {
                Name = dto.Name,
                LastName = dto.LastName,
                Email = dto.Email
            };
        }

        // extension method -> maping AuthorUpdateDTO to Author object (entity)
        public static Author ToAuthor(this AuthorUpdateDTO dto)
        {
            // returns object of type Author
            // creates new Author object and assigns values from AuthorUpdateDTO object
            // mapping values from AuthorUpdateDTO to Author
            return new Author
            {
                Name = dto.Name,
                LastName = dto.LastName,
                Email = dto.Email
            };
        }

    }
}
