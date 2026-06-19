using Application.Features.Categories.Commands.Update;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Categories.Commands.Create
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryValidator() 
        {
            RuleFor(c => c.CategoryId).GreaterThan(0).WithMessage("Invalid category ID.");

            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");

        }
    }
}
