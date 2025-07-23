namespace GadgetHub.API.Models
{
    public class OrderRequestDto
    {
        public int CustomerId { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }

    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public int DistributorId { get; set; }
        public int Quantity { get; set; }
        public decimal AgreedPrice { get; set; }
    }
}
