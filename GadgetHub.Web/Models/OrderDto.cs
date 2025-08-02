using System;

namespace GadgetHub.Web.Models
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal AgreedPrice { get; set; }
        public string CustomerName { get; set; }
        public bool IsDelivered { get; set; }
    }
}