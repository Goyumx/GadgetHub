namespace GadgetHub.API.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int DistributorId { get; set; }

        public int Quantity { get; set; }
        public decimal AgreedPrice { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
        public Distributor Distributor { get; set; }
    }
}
