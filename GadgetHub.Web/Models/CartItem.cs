using System;

namespace GadgetHub.Web.Models
{
    [Serializable]
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
