using Application.Specifications.AuthrSpec;
using Domain.Contracts.UnitOfWorkPattern;
using Domain.Models;
using MediatR;
using static Domain.Exception_Handle.Exceptions;

namespace Application.Features.Authors.Commands.Delete
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAuthorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var authorRepo = _unitOfWork.GetRepository<Author, int>();

            var author = await authorRepo.GetByIdAsync(request.AuthorId);
            if (author == null || author.IsDeleted)
            {
                throw new NotFoundException($"Author with ID {request.AuthorId} was not found.");
            }

            var bookRepo = _unitOfWork.GetRepository<Book, int>();
            var spec = new CheckIfBooksHasSpecificAuthor(request.AuthorId);
            var authorBooks = await bookRepo.GetAllWithSpecAsync(spec);

            if (authorBooks.Any())
            {
                throw new BadRequestException("Cannot delete author because they have active books linked to them.");
            }

            author.IsDeleted = true;
            authorRepo.Update(author);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}