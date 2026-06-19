using Domain.Contracts.SpecificationPattern;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specifications.CategorySpec
{
    public class ActiveCategoriesSpecification : BaseSpecifications<Category, int>
    {
        public ActiveCategoriesSpecification()
        {
            AddCriteria(c => !c.IsDeleted);
        }
    }
}
