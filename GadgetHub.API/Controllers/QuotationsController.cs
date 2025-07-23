using Microsoft.AspNetCore.Mvc;
using GadgetHub.API.Data;
using GadgetHub.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GadgetHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotationsController : ControllerBase
    {
        private readonly GadgetHubDbContext _context;

        public QuotationsController(GadgetHubDbContext context)
        {
            _context = context;
        }

        // POST: api/quotations
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Quotation>>> GetQuotations([FromBody] QuotationRequestDto request)
        {
            var product = await _context.Products.FindAsync(request.ProductId);

            if (product == null)
                return NotFound("Product not found");

            // Simulate responses from 3 distributors
            var distributors = await _context.Distributors.ToListAsync();
            var quotations = new List<Quotation>();

            foreach (var distributor in distributors)
            {
                var random = new Random();

                var availableQty = random.Next(0, request.Quantity + 5); // Simulate inventory
                var unitPrice = product.Price + random.Next(-20, 20);    // Simulate pricing

                var quote = new Quotation
                {
                    ProductId = product.Id,
                    DistributorId = distributor.Id,
                    PricePerUnit = Math.Max(1, unitPrice), // Ensure price > 0
                    AvailableQuantity = availableQty,
                    EstimatedDeliveryDate = DateTime.UtcNow.AddDays(random.Next(2, 10)),
                    Distributor = distributor,
                    Product = product
                };

                quotations.Add(quote);
            }

            // Optionally save to DB if needed
            _context.Quotations.AddRange(quotations);
            await _context.SaveChangesAsync();

            return Ok(quotations);
        }
    }
}
