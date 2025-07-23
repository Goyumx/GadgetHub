using GadgetHub.API.Data;
using GadgetHub.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GadgetHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly GadgetHubDbContext _context;

        public AdminController(GadgetHubDbContext context)
        {
            _context = context;
        }

        // GET: api/admin/summary
        [HttpGet("summary")]
        public async Task<ActionResult> GetSummary()
        {
            var summary = await _context.Distributors
                .Select(d => new
                {
                    Distributor = d.Name,
                    OrdersHandled = _context.OrderItems.Count(o => o.DistributorId == d.Id),
                    TotalEarnings = _context.OrderItems
                        .Where(o => o.DistributorId == d.Id)
                        .Sum(o => o.AgreedPrice * o.Quantity)
                })
                .ToListAsync();

            return Ok(summary);
        }

        // POST: api/admin/add-distributor
        [HttpPost("add-distributor")]
        public async Task<ActionResult> AddDistributor(AuthRequestDto dto)
        {
            if (_context.Distributors.Any(d => d.Username == dto.Username))
                return BadRequest("Username exists");

            _context.Distributors.Add(new Models.Distributor
            {
                Name = dto.Username,
                Username = dto.Username,
                Password = dto.Password
            });

            await _context.SaveChangesAsync();
            return Ok("Distributor added.");
        }
    }
}
