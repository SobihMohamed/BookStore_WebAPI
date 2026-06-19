using Domain.Contracts.SpecificationPattern;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specifications.AuthrSpec
{
    public class AuthorWithBooksSpecification : BaseSpecifications<Author, int>
    {
        public AuthorWithBooksSpecification(int authorId)
        {
            AddCriteria(a => a.Id == authorId && !a.IsDeleted);
            AddInclude(a => a.Books);
        }
    }
}
