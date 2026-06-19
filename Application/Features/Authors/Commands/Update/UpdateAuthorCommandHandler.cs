using Application.Specifications.AuthrSpec;
using Domain.Contracts.UnitOfWorkPattern;
using Domain.Models;
using MediatR;

using static Domain.Exception_Handle.Exceptions;

namespace Application.Features.Authors.Commands.Update
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAuthorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var authorRepo = _unitOfWork.GetRepository<Author, int>();

            var author = await authorRepo.GetByIdAsync(request.AuthorId);
            if (author == null || author.IsDeleted)
            {
                throw new NotFoundException($"Author with ID {request.AuthorId} was not found.");
            }

            if (author.Name.ToLower() != request.Name.ToLower())
            {
                var spec = new CheckAuthorExists(request.Name);
                var nameExists = await authorRepo.GetByIdWithSpecAsync(spec);
                if (nameExists != null)
                {
                    throw new BadRequestException($"Another author with the name '{request.Name}' already exists.");
                }
            }

            author.Name = request.Name;
            author.Bio = request.Bio;

            authorRepo.Update(author);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}