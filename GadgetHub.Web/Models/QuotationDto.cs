using System;

namespace GadgetHub.Web.Models
{
    public class QuotationDto
    {
        public int ProductId { get; set; }
        public int DistributorId { get; set; }
        public decimal PricePerUnit { get; set; }
        public int AvailableQuantity { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }

        public Product Product { get; set; }
        public Distributor Distributor { get; set; }
    }
}
