
using BookCatalog.DTOs.DTOs_Author;
using BookCatalog.Models;

namespace BookCatalog.Mappers
{
    public static class AuthorMapper
    {
        // extension method converts Author to AuthorDTO for api response
        public static AuthorDTO ToAuthorDTO(this Author author)
        {
            return new AuthorDTO
            {
                AuthorDTOId = author.AuthorId,
                Name = author.Name, 
                LastName = author.LastName, 
                Email = author.Email
            };
        }

        // extension method converts Author to AuthorDetailDTO for api response
        public static AuthorDetailDTO ToAuthorDetailDTO(this Author author)
        {
            return new AuthorDetailDTO
            {
                AuthorDetailDTOId = author.AuthorId,
                Name = author.Name,
                LastName = author.LastName,
                Email = author.Email,
                PhoneNumber = author.PhoneNumber
            };
        }

        // extension method converts AuthorCreateDTO to Author for database
        public static Author ToAuthor(this AuthorCreateDTO dto)
        {
            return new Author
            {
                Name = dto.Name,
                LastName = dto.LastName,
                Email = dto.Email
            };
        }

        // extension method converts AuthorUpdateDTO to Author for database
        public static Author ToAuthor(this AuthorUpdateDTO dto)
        {
            return new Author
            {
                Name = dto.Name,
                LastName = dto.LastName,
                Email = dto.Email
            };
        }

    }
}
