using Domain.Contracts.SpecificationPattern;
using Domain.Models;

namespace Application.Specifications.CategorySpec
{
    public class CheckActiveCategoryByIdSpec : BaseSpecifications<Category, int>
    {
        public CheckActiveCategoryByIdSpec(int categoryId)
        {
            AddCriteria(c => c.Id == categoryId && !c.IsDeleted);
        }
    }
}