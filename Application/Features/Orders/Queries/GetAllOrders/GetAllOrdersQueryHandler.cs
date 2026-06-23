using Application.DTOs.Order;
using Application.Specifications.OrderSpec;
using Domain.Contracts.UnitOfWorkPattern;
using Domain.Models;
using MediatR;

namespace Application.Features.Orders.Queries.GetAllOrders
{
    public record GetAllOrdersQuery() : IRequest<IEnumerable<OrderDto>>;
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllOrdersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orderRepo = _unitOfWork.GetRepository<Order, int>();

            var spec = new OrderWithItemsSpecification();
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