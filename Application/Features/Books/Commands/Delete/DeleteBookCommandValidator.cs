using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Books.Commands.Delete
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(x => x.BookId).GreaterThan(0).WithMessage("Invalid Book ID.");
        }
    }
}
