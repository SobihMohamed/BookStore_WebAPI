using Domain.Contracts.SpecificationPattern;
using Domain.Models;

namespace Application.Specifications.AuthrSpec
{
    public class CheckAuthorExists : BaseSpecifications<Author ,int>
    {
        public CheckAuthorExists(string name)
        {
            AddCriteria(a => a.Name.ToLower() == name.ToLower() && !a.IsDeleted);
        }
    }
}