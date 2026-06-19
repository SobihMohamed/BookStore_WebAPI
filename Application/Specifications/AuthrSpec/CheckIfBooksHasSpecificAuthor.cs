using Domain.Contracts.SpecificationPattern;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specifications.AuthrSpec
{
    public class CheckIfBooksHasSpecificAuthor : BaseSpecifications<Book, int>
    {
        public CheckIfBooksHasSpecificAuthor(int authorId)
        {
            AddCriteria(b => b.AuthorId == authorId && !b.IsDeleted);
        }
    }
}
