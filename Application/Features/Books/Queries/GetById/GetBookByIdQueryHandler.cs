using Application.DTOs.Book;
using Application.Specifications.BookSpec;
using Domain.Contracts.UnitOfWorkPattern;
using Domain.Models;
using MediatR;
using static Domain.Exception_Handle.Exceptions;

namespace Application.Features.Books.Queries.GetById
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDetailsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetBookByIdQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<BookDetailsDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var bookRepo = _unitOfWork.GetRepository<Book, int>();
            var spec = new BookWithIncludesSpecification(request.BookId);
            var book = await bookRepo.GetByIdWithSpecAsync(spec);

            if (book == null) throw new NotFoundException($"Book with ID {request.BookId} was not found.");

            return new BookDetailsDto
            {
                Id = book.Id,
                Title = book.Title,
                Price = book.Price,
                Stock = book.Stock,
                CategoryName = book.Category?.Name ?? "",
                AuthorName = book.Author?.Name ?? "",
                BookType = book switch { Paperback => "Paperback", EBook => "EBook", _ => "Unknown" },
                ShippingWeight = book is Paperback p ? p.ShippingWeight : null,
                DownloadUrl = book is EBook e ? e.DownloadUrl : null,
                FileSizeMB = book is EBook eb ? eb.FileSizeMB : null
            };
        }
    }
}
