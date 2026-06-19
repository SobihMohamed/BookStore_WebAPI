using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Authors.Commands.Update
{
    public class UpdateAuthorCommand : IRequest<bool>
    {
        public int AuthorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Bio { get; set; }
    }
}
