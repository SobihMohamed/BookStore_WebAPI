using Application.DTOs.Book;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Category
{
    public class CategoryWithBooksDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public IEnumerable<BookDetailsDto> Books { get; set; } = new List<BookDetailsDto>();
    }
}
