using Domain.Contracts.SpecificationPattern;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specifications.BookSpec
{
    public class BookWithIncludesSpecification : BaseSpecifications<Book, int>
    {
        public BookWithIncludesSpecification(int id)
        {
            AddCriteria(b => b.Id == id && !b.IsDeleted);
            AddInclude(b => b.Category);
            AddInclude(b => b.Author);
        }
    }
}
