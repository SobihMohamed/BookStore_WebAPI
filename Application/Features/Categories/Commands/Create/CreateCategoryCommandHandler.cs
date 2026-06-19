using Application.Specifications.CategorySpec;
using Domain.Contracts.UnitOfWorkPattern;
using Domain.Models;
using MediatR;
using static Domain.Exception_Handle.Exceptions;

namespace Application.Features.Categories.Commands.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryRepo = _unitOfWork.GetRepository<Category, int>();

            var spec = new CheckExistingCategory(request.Name);
            var existingCategory = await categoryRepo.GetByIdWithSpecAsync(spec);
            if (existingCategory != null)
            {
                throw new BadRequestException($"A category with the name '{request.Name}' already exists.");
            }

            var category = new Category
            {
                Name = request.Name
            };

            await categoryRepo.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return category.Id;
        }
    }
}
