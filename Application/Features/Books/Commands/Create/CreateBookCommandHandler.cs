using Application.Specifications.AuthorSpec;
using Application.Specifications.CategorySpec;
using Domain.Contracts.UnitOfWorkPattern;
using Domain.Models;
using MediatR;
using static Domain.Exception_Handle.Exceptions;

namespace Application.Features.Books.Commands.Create
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var authorRepo = _unitOfWork.GetRepository<Author, int>();
            var categoryRepo = _unitOfWork.GetRepository<Category, int>();

            var authorSpec = new CheckActiveAuthorByIdSpec(request.AuthorId);
            var authorExists = await authorRepo.GetByIdWithSpecAsync(authorSpec);
            if (authorExists == null)
            {
                throw new BadRequestException($"Author with ID {request.AuthorId} does not exist or is deleted.");
            }

            var categorySpec = new CheckActiveCategoryByIdSpec(request.CategoryId);
            var categoryExists = await categoryRepo.GetByIdWithSpecAsync(categorySpec);
            if (categoryExists == null)
            {
                throw new BadRequestException($"Category with ID {request.CategoryId} does not exist or is deleted.");
            }

            Book newBook;

            if (request.BookType == "Paperback")
            {
                newBook = new Paperback
                {
                    Title = request.Title,
                    Price = request.Price,
                    Stock = request.Stock,
                    AuthorId = request.AuthorId,
                    CategoryId = request.CategoryId,
                    ShippingWeight = request.ShippingWeight!.Value 
                };
            }
            else 
            {
                newBook = new EBook
                {
                    Title = request.Title,
                    Price = request.Price,
                    Stock = request.Stock,
                    AuthorId = request.AuthorId,
                    CategoryId = request.CategoryId,
                    DownloadUrl = request.DownloadUrl!,
                    FileSizeMB = request.FileSizeMB!.Value
                };
            }

            var bookRepo = _unitOfWork.GetRepository<Book, int>();
            await bookRepo.AddAsync(newBook);
            await _unitOfWork.SaveChangesAsync();

            return newBook.Id;
        }
    }
}