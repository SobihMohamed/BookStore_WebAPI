using Application.DTOs.Order;
using Application.Specifications.OrderSpec;
using Domain.Contracts.UnitOfWorkPattern;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Orders.Queries.GetCustomerOrders
{
    // use record for the query to make it immutable and  
    public record GetCustomerOrdersQuery(int CustomerId) : IRequest<IEnumerable<OrderDto>>;
    public class GetCustomerOrdersQueryHandler : IRequestHandler<GetCustomerOrdersQuery, IEnumerable<OrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCustomerOrdersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<OrderDto>> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            var orderRepo = _unitOfWork.GetRepository<Order, int>();

            var spec = new OrderWithItemsSpecification(request.CustomerId);
            var orders = await orderRepo.GetAllWithSpecAsync(spec);

            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                CustomerName = o.Customer?.FullName ?? "Unknown",
                Items = o.OrderItems.Select(i => new OrderItemDto
                {
                    BookId = i.BookId,
                    BookTitle = i.Book?.Title ?? "Unknown Book",
                    Quantity = i.Quantity,
                    UnitPriceAtPurchase = i.UnitPriceAtPurchase
                }).ToList()
            });
        }
    }
}
}
