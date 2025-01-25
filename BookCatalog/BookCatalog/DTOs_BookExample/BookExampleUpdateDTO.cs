﻿using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DTOs_BookExample
{
    public class BookExampleUpdateDTO
    {
        [Key]
        public int BookExampleId { get; set; }

        [Required]
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string ISBN { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}
