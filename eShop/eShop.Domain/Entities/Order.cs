using System;
using System.Collections.Generic;

namespace eShop.Domain
{
    public class Order : Entity
    {
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime PlacedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }

        public decimal TotalPrice { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}