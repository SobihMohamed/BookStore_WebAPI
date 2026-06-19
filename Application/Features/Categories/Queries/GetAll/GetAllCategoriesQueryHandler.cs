using Application.DTOs.Category;
using Application.Specifications.CategorySpec;
using Domain.Contracts.UnitOfWorkPattern;
using Domain.Models;
using MediatR;
namespace Application.Features.Categories.Queries.GetAll
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categoryRepo = _unitOfWork.GetRepository<Category, int>();

            var spec = new ActiveCategoriesSpecification();
            var categories = await categoryRepo.GetAllWithSpecAsync(spec);

            var categoryDtos = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            });

            return categoryDtos;
        }
    }
}