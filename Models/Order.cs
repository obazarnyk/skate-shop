using System;
using System.Collections.Generic;
using educational_practice5.Authentication;

namespace educational_practice5.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public string UserId { get; set; }

        public List<OrderForm> Products { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}