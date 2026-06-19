using Domain.Contracts.SpecificationPattern;
using Domain.Models;

namespace Application.Specifications.CategorySpec
{
    public class CheckExistingCategory : BaseSpecifications<Category, int>
    {
        public CheckExistingCategory(string categoryName)
        {
            AddCriteria(c => c.Name.ToLower() == categoryName.ToLower() && !c.IsDeleted);
        }
    }
}