using Application.Common;
using Application.DTOs.Book;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Books.Queries.GetAll
{
    public record GetAllBooksQuery(BookSpecParams Params) : IRequest<PaginationResponse<BookDetailsDto>>
    {

    }
}
