using Application.Specifications.AuthrSpec;
using Domain.Contracts.UnitOfWorkPattern;
using Domain.Models;
using MediatR;
using static Domain.Exception_Handle.Exceptions;

namespace Application.Features.Authors.Commands.Create
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAuthorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var authorRepo = _unitOfWork.GetRepository<Author, int>();

            var spec = new CheckAuthorExists(request.Name);
            var existingAuthor = await authorRepo.GetByIdWithSpecAsync(spec);

            if (existingAuthor != null)
            {
                throw new BadRequestException($"An author with the name '{request.Name}' already exists.");
            }

            var author = new Author
            {
                Name = request.Name,
                Bio = request.Bio
            };

            await authorRepo.AddAsync(author);
            await _unitOfWork.SaveChangesAsync();

            return author.Id;
        }
    }
}