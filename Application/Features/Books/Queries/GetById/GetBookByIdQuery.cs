using Application.DTOs.Book;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Books.Queries.GetById
{
    public record GetBookByIdQuery(int BookId) : IRequest<BookDetailsDto>
    {
    }
}
