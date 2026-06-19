using Application.DTOs.Category;
using MediatR;

namespace Application.Features.Categories.Queries.GetAll
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDto>>
    {
    }
}
