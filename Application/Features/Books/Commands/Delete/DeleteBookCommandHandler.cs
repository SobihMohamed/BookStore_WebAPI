using Domain.Contracts.UnitOfWorkPattern;
using Domain.Models;
using MediatR;
using static Domain.Exception_Handle.Exceptions;

namespace Application.Features.Books.Commands.Delete
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var bookRepo = _unitOfWork.GetRepository<Book, int>();

            var book = await bookRepo.GetByIdAsync(request.BookId);

            if (book == null || book.IsDeleted)
            {
                throw new NotFoundException($"Book with ID {request.BookId} was not found.");
            }

            book.IsDeleted = true;

            bookRepo.Update(book);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}