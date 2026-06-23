using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    }
}
