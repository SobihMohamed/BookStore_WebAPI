using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Orders.Commands.Create
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId).GreaterThan(0).WithMessage("Invalid Customer ID.");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Order must contain at least one item.");

            RuleForEach(x => x.Items).ChildRules(items =>
            {
                items.RuleFor(i => i.BookId).GreaterThan(0).WithMessage("Invalid Book ID.");
                items.RuleFor(i => i.Quantity).GreaterThan(0).WithMessage("Quantity must be at least 1.");
            });
        }
    }
}