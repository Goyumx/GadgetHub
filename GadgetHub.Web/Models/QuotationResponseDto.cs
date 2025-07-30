using System;

namespace GadgetHub.Web.Models
{
    [Serializable]
    public class QuotationResponseDto
    {
        public string ProductName { get; set; }
        public string DistributorName { get; set; }
        public decimal PricePerUnit { get; set; }
        public int AvailableQuantity { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
    }
}
