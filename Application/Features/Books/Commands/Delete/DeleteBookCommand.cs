using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Books.Commands.Delete
{
    public class DeleteBookCommand : IRequest<bool>
    {
        public int BookId { get; set; }
    }
}
