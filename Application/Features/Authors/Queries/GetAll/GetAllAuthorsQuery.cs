using Application.DTOs.Author;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Authors.Queries.GetAll
{
    public class GetAllAuthorsQuery : IRequest<IEnumerable<AuthorDto>> 
    {
    
    }
}
