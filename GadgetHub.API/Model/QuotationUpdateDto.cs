namespace GadgetHub.API.Models
{
    public class QuotationUpdateDto
    {
        public int QuotationId { get; set; }
        public decimal PricePerUnit { get; set; }
        public int AvailableQuantity { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
    }
}
