using Domain.Contracts.SpecificationPattern;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specifications.CategorySpec
{
    public class CategoryWithBooksSpecification : BaseSpecifications<Category, int>
    {
        public CategoryWithBooksSpecification(int categoryId)
        {
            AddCriteria(c => c.Id == categoryId && !c.IsDeleted);

            AddInclude(c => c.Books);
        }
    }
}
