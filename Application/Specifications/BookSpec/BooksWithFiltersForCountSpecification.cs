using Domain.Contracts.SpecificationPattern;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specifications.BookSpec
{
    public class BooksWithFiltersForCountSpecification : BaseSpecifications<Book, int>
    {
        public BooksWithFiltersForCountSpecification(BookSpecParams bookParams)
        {
            AddCriteria(b =>
                !b.IsDeleted &&
                (string.IsNullOrEmpty(bookParams.Search) || b.Title.ToLower().Contains(bookParams.Search)) &&
                (!bookParams.CategoryId.HasValue || b.CategoryId == bookParams.CategoryId) &&
                (!bookParams.AuthorId.HasValue || b.AuthorId == bookParams.AuthorId)
            );
        }
    }
}
