using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Authors.Commands.Delete
{
    public class DeleteAuthorCommand : IRequest<bool>
    {
        public int AuthorId { get; set; }
    }
}
