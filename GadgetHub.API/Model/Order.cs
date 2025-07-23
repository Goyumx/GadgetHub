using System.ComponentModel.DataAnnotations;

namespace GadgetHub.API.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public bool IsDelivered { get; set; }

        public Customer Customer { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
