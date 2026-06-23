using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Books.Commands.Update
{
    public class UpdateBookCommand : IRequest<bool>
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }

        public string BookType { get; set; } = string.Empty;

        public decimal? ShippingWeight { get; set; }
        public string? DownloadUrl { get; set; }
        public int? FileSizeMB { get; set; }
    }
}
