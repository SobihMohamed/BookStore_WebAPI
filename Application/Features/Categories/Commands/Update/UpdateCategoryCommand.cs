using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Categories.Commands.Update
{
    public class UpdateCategoryCommand : IRequest<bool>
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
