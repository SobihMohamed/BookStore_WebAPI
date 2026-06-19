using FluentValidation;


namespace Application.Features.Categories.Commands.Delete
{
    public class DeleteCategoryValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryValidator()
        {
            RuleFor(c => c.CategoryId).GreaterThan(0).WithMessage("Invalid category ID.");
        }
    }
}
