﻿using System.ComponentModel.DataAnnotations;
using BookCatalog.DTOs.DTOs_Author;

namespace BookCatalog.DTOs.DTOs_Book
{
    // book detail (Data Transfer Object)
    public class BookDetailDTO
    {
        // properties
        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public string Description { get; set; } = string.Empty;

        public int Year { get; set; }

        public string Status { get; set; } = null!;

        public ICollection<AuthorDTO> Authors { get; set; } = new List<AuthorDTO>();
    }
}
