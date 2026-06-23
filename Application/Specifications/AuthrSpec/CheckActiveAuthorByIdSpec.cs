using Domain.Contracts.SpecificationPattern;
using Domain.Models;

namespace Application.Specifications.AuthorSpec
{
    public class CheckActiveAuthorByIdSpec : BaseSpecifications<Author, int>
    {
        public CheckActiveAuthorByIdSpec(int authorId)
        {
            AddCriteria(a => a.Id == authorId && !a.IsDeleted);
        }
    }
}