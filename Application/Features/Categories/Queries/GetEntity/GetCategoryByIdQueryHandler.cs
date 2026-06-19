using Application.DTOs.Book;
using Application.DTOs.Category;
using Application.Specifications.CategorySpec;
using Domain.Contracts.UnitOfWorkPattern;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Exception_Handle.Exceptions;

namespace Application.Features.Categories.Queries.GetEntity
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryWithBooksDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryWithBooksDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var categoryRepo = _unitOfWork.GetRepository<Category, int>();

            var spec = new CategoryWithBooksSpecification(request.CategoryId);
            var category = await categoryRepo.GetByIdWithSpecAsync(spec);

            if (category == null)
            {
                throw new NotFoundException($"Category with ID {request.CategoryId} was not found.");
            }

            var categoryDto = new CategoryWithBooksDto
            {
                Id = category.Id,
                Name = category.Name,

                Books = category.Books
                    .Select(b => new BookDto
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Price = b.Price,

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

            return categoryDto;
        }
    }
}