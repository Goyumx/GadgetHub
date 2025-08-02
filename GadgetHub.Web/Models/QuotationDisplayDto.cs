using System;

namespace GadgetHub.Web.Models
{
    public class QuotationDisplayDto
    {
        public int QuotationId { get; set; }           // <-- Needed to respond to quotation
        public string ProductName { get; set; }        // <-- For display in GridView
        public decimal PricePerUnit { get; set; }
        public int AvailableQuantity { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
    }
}
