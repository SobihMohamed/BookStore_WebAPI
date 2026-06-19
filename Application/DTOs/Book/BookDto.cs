using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Book
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string BookType { get; set; } = string.Empty; 
        public decimal? ShippingWeight { get; set; }
        public string? DownloadUrl { get; set; }
        public int? FileSizeMB { get; set; }
    }
}
