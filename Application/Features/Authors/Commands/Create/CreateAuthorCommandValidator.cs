using FluentValidation;

namespace Application.Features.Authors.Commands.Create
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Author name is required.")
                .MaximumLength(150).WithMessage("Author name must not exceed 150 characters.");

            RuleFor(x => x.Bio)
                .MaximumLength(1000).WithMessage("Biography must not exceed 1000 characters.");
        }
    }
}