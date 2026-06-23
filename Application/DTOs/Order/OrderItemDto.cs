using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Order
{
    public class OrderItemDto
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPriceAtPurchase { get; set; }
        public decimal TotalItemPrice => Quantity * UnitPriceAtPurchase;
    }
}
