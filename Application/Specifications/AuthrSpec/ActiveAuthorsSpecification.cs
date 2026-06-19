using Domain.Contracts.SpecificationPattern;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specifications.AuthrSpec
{
    public class ActiveAuthorsSpecification : BaseSpecifications<Author, int>
    {
        public ActiveAuthorsSpecification()
        {
            AddCriteria(a => !a.IsDeleted);
        }
    }
}
