using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Orders.Commands.Create
{
    public class CreateOrderCommand : IRequest<int>
    {
        public int CustomerId { get; set; }
        public List<OrderItemRequest> Items { get; set; } = new List<OrderItemRequest>();
    }

    public class OrderItemRequest
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}
