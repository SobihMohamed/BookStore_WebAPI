using FluentValidation;

namespace Application.Features.Authors.Commands.Update
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(x => x.AuthorId)
                .GreaterThan(0).WithMessage("Invalid Author ID.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Author name is required.")
                .MaximumLength(150).WithMessage("Author name must not exceed 150 characters.");
            
            RuleFor(x => x.Bio)
                .MaximumLength(1000).WithMessage("Biography must not exceed 1000 characters.");
        }
    }
}