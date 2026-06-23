using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Books.Commands.Update
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(x => x.BookId).GreaterThan(0).WithMessage("Invalid Book ID.");
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Stock).GreaterThanOrEqualTo(0);
            RuleFor(x => x.AuthorId).GreaterThan(0);
            RuleFor(x => x.CategoryId).GreaterThan(0);

            RuleFor(x => x.BookType)
                .NotEmpty()
                .Must(type => type == "Paperback" || type == "EBook")
                .WithMessage("BookType must be either 'Paperback' or 'EBook'.");

            When(x => x.BookType == "Paperback", () =>
            {
                RuleFor(x => x.ShippingWeight)
                    .NotNull().WithMessage("Shipping Weight is required for Paperback books.")
                    .GreaterThan(0);
            });

            When(x => x.BookType == "EBook", () =>
            {
                RuleFor(x => x.DownloadUrl).NotEmpty().WithMessage("Download URL is required for EBooks.");
                RuleFor(x => x.FileSizeMB)
                    .NotNull().WithMessage("File Size is required for EBooks.")
                    .GreaterThan(0);
            });
        }
    }
}