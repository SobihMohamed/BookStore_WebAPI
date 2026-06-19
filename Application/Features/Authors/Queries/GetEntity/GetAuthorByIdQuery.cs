using Application.DTOs.Author;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Authors.Queries.GetEntity
{
    public record GetAuthorByIdQuery(int AuthorId) : IRequest<AuthorWithBooksDto>;
}
