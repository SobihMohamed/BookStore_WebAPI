using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Author
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
    }
}
