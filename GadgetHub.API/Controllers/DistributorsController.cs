using Microsoft.AspNetCore.Mvc;
using GadgetHub.API.Data;
using GadgetHub.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GadgetHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DistributorsController : ControllerBase
    {
        private readonly GadgetHubDbContext _context;

        public DistributorsController(GadgetHubDbContext context)
        {
            _context = context;
        }

        // POST: api/distributors/login
        [HttpPost("login")]
        public async Task<ActionResult> Login(AuthRequestDto request)
        {
            var distributor = await _context.Distributors
                .FirstOrDefaultAsync(d => d.Username == request.Username && d.Password == request.Password);

            if (distributor == null)
                return Unauthorized("Invalid credentials.");

            return Ok(new
            {
                Message = "Login successful",
                DistributorId = distributor.Id,
                distributor.Name
            });
        }

        // GET: api/distributors/{id}/quotations
        [HttpGet("{id}/quotations")]
        public async Task<ActionResult> GetQuotationsForDistributor(int id)
        {
            var quotes = await _context.Quotations
                .Include(q => q.Product)
                .Where(q => q.DistributorId == id)
                .ToListAsync();

            return Ok(quotes);
        }

        // PUT: api/distributors/respond
        [HttpPut("respond")]
        public async Task<ActionResult> RespondToQuotation([FromBody] QuotationUpdateDto response)
        {
            var quotation = await _context.Quotations.FindAsync(response.QuotationId);
            if (quotation == null) return NotFound("Quotation not found");

            quotation.PricePerUnit = response.PricePerUnit;
            quotation.AvailableQuantity = response.AvailableQuantity;
            quotation.EstimatedDeliveryDate = response.EstimatedDeliveryDate;

            await _context.SaveChangesAsync();
            return Ok("Response submitted.");
        }
        // GET: api/distributors/{id}/orders
        [HttpGet("{id}/orders")]
        public async Task<ActionResult> GetOrdersForDistributor(int id)
        {
            var orders = await _context.OrderItems
                .Include(o => o.Order)
                .Include(o => o.Product)
                .Where(o => o.DistributorId == id)
                .Select(o => new
                {
                    OrderId = o.OrderId,
                    Product = o.Product.Name,
                    o.Quantity,
                    o.AgreedPrice,
                    o.Order.IsDelivered,
                    o.Order.OrderDate
                })
                .ToListAsync();

            return Ok(orders);
        }

        // PUT: api/distributors/mark-delivered/{orderId}
        [HttpPut("mark-delivered/{orderId}")]
        public async Task<ActionResult> MarkDelivered(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return NotFound("Order not found");

            order.IsDelivered = true;
            await _context.SaveChangesAsync();

            return Ok("Order marked as delivered.");
        }

    }
}
