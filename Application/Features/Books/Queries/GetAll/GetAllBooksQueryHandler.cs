using Application.Common;
using Application.DTOs.Book;
using Application.Specifications.BookSpec;
using Domain.Contracts.UnitOfWorkPattern;
using Domain.Models;
using MediatR;
namespace Application.Features.Books.Queries.GetAll
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, PaginationResponse<BookDetailsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllBooksQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<PaginationResponse<BookDetailsDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var bookRepo = _unitOfWork.GetRepository<Book, int>();

            var countSpec = new BooksWithFiltersForCountSpecification(request.Params);
            var totalItems = await bookRepo.GetCountAsync(countSpec);

            var spec = new BooksWithPaginationAndFilteringSpecification(request.Params);
            var books = await bookRepo.GetAllWithSpecAsync(spec);

            var data = books.Select(book => new BookDetailsDto
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
            }).ToList();

            return new PaginationResponse<BookDetailsDto>(request.Params.PageIndex, request.Params.PageSize, totalItems, data);
        }
    }
}