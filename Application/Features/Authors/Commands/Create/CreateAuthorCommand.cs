using MediatR;

namespace Application.Features.Authors.Commands.Create
{
    public class CreateAuthorCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
        public string? Bio { get; set; }
    }
}