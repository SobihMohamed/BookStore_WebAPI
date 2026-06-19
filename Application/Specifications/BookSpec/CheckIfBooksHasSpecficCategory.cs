using Domain.Contracts.SpecificationPattern;
using Domain.Models;

namespace Application.Specifications.BookSpec
{
    public class CheckIfBooksHasSpecficCategory : BaseSpecifications<Book, int>
    {
        public CheckIfBooksHasSpecficCategory(int categoryId)
        {
            AddCriteria(b => b.CategoryId == categoryId && !b.IsDeleted);
        }
    }
}