using Domain.Contracts.SpecificationPattern;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specifications.BookSpec
{
    public class BooksWithPaginationAndFilteringSpecification : BaseSpecifications<Book, int>
    {
        public BooksWithPaginationAndFilteringSpecification(BookSpecParams bookParams)
        {
            AddCriteria(b =>
                !b.IsDeleted &&
                (string.IsNullOrEmpty(bookParams.Search) || b.Title.ToLower().Contains(bookParams.Search)) &&
                (!bookParams.CategoryId.HasValue || b.CategoryId == bookParams.CategoryId) &&
                (!bookParams.AuthorId.HasValue || b.AuthorId == bookParams.AuthorId)
            );

            AddInclude(b => b.Category);
            AddInclude(b => b.Author);

            ApplyPagenation(bookParams.PageSize , bookParams.PageIndex);
        }
    }
}
