using Application.DTOs.Book;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Author
{
    public class AuthorWithBooksDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public IEnumerable<BookDetailsDto> Books { get; set; } = new List<BookDetailsDto>();
    }
}
