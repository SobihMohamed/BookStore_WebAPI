using Application.Specifications.AuthorSpec;
using Application.Specifications.CategorySpec;
using Domain.Contracts.UnitOfWorkPattern;
using Domain.Models;
using MediatR;
using static Domain.Exception_Handle.Exceptions;

namespace Application.Features.Books.Commands.Update
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var bookRepo = _unitOfWork.GetRepository<Book, int>();
            var authorRepo = _unitOfWork.GetRepository<Author, int>();
            var categoryRepo = _unitOfWork.GetRepository<Category, int>();

            var book = await bookRepo.GetByIdAsync(request.BookId);
            if (book == null || book.IsDeleted)
            {
                throw new NotFoundException($"Book with ID {request.BookId} was not found.");
            }

            if (book.AuthorId != request.AuthorId)
            {
                var authorExists = await authorRepo.GetByIdWithSpecAsync(new CheckActiveAuthorByIdSpec(request.AuthorId));
                if (authorExists == null) throw new BadRequestException("Invalid or deleted Author.");
            }

            if (book.CategoryId != request.CategoryId)
            {
                var categoryExists = await categoryRepo.GetByIdWithSpecAsync(new CheckActiveCategoryByIdSpec(request.CategoryId));
                if (categoryExists == null) throw new BadRequestException("Invalid or deleted Category.");
            }

            book.Title = request.Title;
            book.Price = request.Price;
            book.Stock = request.Stock;
            book.AuthorId = request.AuthorId;
            book.CategoryId = request.CategoryId;

            if (book is Paperback paperback)
            {
                if (request.BookType != "Paperback")
                    throw new BadRequestException("Cannot change a Paperback book to an EBook.");

                paperback.ShippingWeight = request.ShippingWeight!.Value;
            }
            else if (book is EBook eBook)
            {
                if (request.BookType != "EBook")
                    throw new BadRequestException("Cannot change an EBook to a Paperback book.");

                eBook.DownloadUrl = request.DownloadUrl!;
                eBook.FileSizeMB = request.FileSizeMB!.Value;
            }

            bookRepo.Update(book);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}