namespace GadgetHub.API.Models
{
    public class Quotation
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int DistributorId { get; set; }

        public decimal PricePerUnit { get; set; }
        public int AvailableQuantity { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }

        // Navigation
        public Product Product { get; set; }
        public Distributor Distributor { get; set; }
    }
}
