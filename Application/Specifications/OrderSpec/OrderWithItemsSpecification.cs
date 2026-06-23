using Domain.Contracts.SpecificationPattern;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specifications.OrderSpec
{
    public class OrderWithItemsSpecification : BaseSpecifications<Order, int>
    {
        public OrderWithItemsSpecification()
        {
            AddInclude(o => o.Customer);
            AddInclude(o => o.OrderItems);
            var bookInclude = $"{nameof(Order.OrderItems)}.{nameof(OrderItem.Book)}";
            AddInclude(bookInclude);
        }

        public OrderWithItemsSpecification(int customerId)
        {
            AddCriteria(o => o.CustomerId == customerId);
            AddInclude(o => o.Customer);
            AddInclude(o => o.OrderItems);
            var bookInclude = $"{nameof(Order.OrderItems)}.{nameof(OrderItem.Book)}";
            AddInclude(bookInclude);
        }
    }
}
