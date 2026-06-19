using Application.DTOs.Category;
using MediatR;

namespace Application.Features.Categories.Queries.GetEntity
{
    public record GetCategoryByIdQuery(int CategoryId) : IRequest<CategoryWithBooksDto>
    {

    }
}
