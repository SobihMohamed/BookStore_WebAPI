using Application.Specifications.CategorySpec;
using Domain.Contracts.UnitOfWorkPattern;
using Domain.Models;
using MediatR;
using static Domain.Exception_Handle.Exceptions;

namespace Application.Features.Categories.Commands.Update
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryRepo = _unitOfWork.GetRepository<Category, int>();

            var category = await categoryRepo.GetByIdAsync(request.CategoryId);
            if (category == null)
            {
                throw new NotFoundException($"Category with ID {request.CategoryId} was not found.");
            }

            if (category.Name.ToLower() != request.Name.ToLower())
            {
                var spec = new CheckExistingCategory(request.Name);

                var nameExists = await categoryRepo.GetByIdWithSpecAsync(spec);
                if (nameExists != null)
                {
                    throw new BadRequestException($"Another category with the name '{request.Name}' already exists.");
                }
            }

            category.Name = request.Name;

            categoryRepo.Update(category);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}