using Application.DTOs.Author;
using Application.DTOs.Book;
using Application.Specifications.AuthrSpec;
using Domain.Contracts.UnitOfWorkPattern;
using Domain.Models;
using MediatR;

using static Domain.Exception_Handle.Exceptions;

namespace Application.Features.Authors.Queries.GetEntity
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, AuthorWithBooksDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAuthorByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthorWithBooksDto> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var authorRepo = _unitOfWork.GetRepository<Author, int>();

            var spec = new AuthorWithBooksSpecification(request.AuthorId);
            var author = await authorRepo.GetByIdWithSpecAsync(spec);

            if (author == null)
            {
                throw new NotFoundException($"Author with ID {request.AuthorId} was not found.");
            }

            var authorDto = new AuthorWithBooksDto
            {
                Id = author.Id,
                Name = author.Name,
                Bio = author.Bio,
                Books = author.Books
                    .Where(b => !b.IsDeleted) 
                    .Select(b => new BookDetailsDto
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Price = b.Price,
                        Stock = b.Stock,
                        BookType = b switch
                        {
                            Paperback => "Paperback",
                            EBook => "EBook",
                            _ => "Unknown"
                        },
                        ShippingWeight = b is Paperback paperback ? paperback.ShippingWeight : null,
                        DownloadUrl = b is EBook eBook ? eBook.DownloadUrl : null,
                        FileSizeMB = b is EBook eb ? eb.FileSizeMB : null
                    }).ToList()
            };

            return authorDto;
        }
    }
}