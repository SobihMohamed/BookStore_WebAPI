using Application.Specifications.BookSpec;
using Domain.Contracts.UnitOfWorkPattern;
using Domain.Models;
using MediatR;
using static Domain.Exception_Handle.Exceptions;

namespace Application.Features.Categories.Commands.Delete
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryRepo = _unitOfWork.GetRepository<Category, int>();

            var category = await categoryRepo.GetByIdAsync(request.CategoryId);

            if (category == null || category.IsDeleted)
            {
                throw new NotFoundException($"Category with ID {request.CategoryId} was not found.");
            }

            var bookRepo = _unitOfWork.GetRepository<Book, int>();
            var spec = new CheckIfBooksHasSpecficCategory(request.CategoryId);
            var Books = await bookRepo.GetAllWithSpecAsync(spec);

            if (Books.Any())
                throw new BadRequestException("Cannot delete category because it contains active books.");

            category.IsDeleted = true;
            categoryRepo.Update(category);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}