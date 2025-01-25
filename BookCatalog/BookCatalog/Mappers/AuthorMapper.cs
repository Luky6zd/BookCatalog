using BookCatalog.DTO_s;
using BookCatalog.DTOs_Author;
using BookCatalog.Models;

namespace BookCatalog.Mappers
{
    public static class AuthorMapper
    {
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

        public static Author ToAuthor(this AuthorCreateDTO dto)
        {
            return new Author
            {
                Name = dto.Name,
                LastName = dto.LastName,
                Email = dto.Email
            };
        }

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
