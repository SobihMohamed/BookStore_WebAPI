using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Authors.Commands.Delete
{
    public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(x => x.AuthorId).GreaterThan(0).WithMessage("Invalid Author ID.");
        }
    }
}
