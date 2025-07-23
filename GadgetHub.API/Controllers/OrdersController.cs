using Microsoft.AspNetCore.Mvc;
using GadgetHub.API.Data;
using GadgetHub.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GadgetHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly GadgetHubDbContext _context;

        public OrdersController(GadgetHubDbContext context)
        {
            _context = context;
        }

        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult> PlaceOrder([FromBody] OrderRequestDto request)
        {
            var customer = await _context.Customers.FindAsync(request.CustomerId);
            if (customer == null) return NotFound("Customer not found");

            var order = new Order
            {
                CustomerId = request.CustomerId,
                OrderDate = DateTime.UtcNow,
                IsDelivered = false,
                Items = new List<OrderItem>()
            };

            foreach (var item in request.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                var distributor = await _context.Distributors.FindAsync(item.DistributorId);

                if (product == null || distributor == null)
                    return BadRequest("Invalid product or distributor.");

                order.Items.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    DistributorId = item.DistributorId,
                    Quantity = item.Quantity,
                    AgreedPrice = item.AgreedPrice
                });
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Order Placed Successfully 🎉",
                OrderId = order.Id,
                Total = order.Items.Sum(i => i.Quantity * i.AgreedPrice)
            });
        }
    }
}
