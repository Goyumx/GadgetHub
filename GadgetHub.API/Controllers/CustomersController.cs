using Microsoft.AspNetCore.Mvc;
using GadgetHub.API.Data;
using GadgetHub.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GadgetHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly GadgetHubDbContext _context;

        public CustomersController(GadgetHubDbContext context)
        {
            _context = context;
        }

        // POST: api/customers/register
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] CustomerRegisterDto request)
        {
            if (await _context.Customers.AnyAsync(c => c.Username == request.Username))
                return BadRequest("Username already exists.");

            var customer = new Customer
            {
                Username = request.Username,
                Password = request.Password, // (to be hashed later)
                Name = request.Name,
                Phone = request.Phone,
                Address = request.Address
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok("Registration successful.");
        }

        // POST: api/customers/login
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] AuthRequestDto request)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Username == request.Username && c.Password == request.Password);

            if (customer == null)
                return Unauthorized("Invalid credentials.");

            return Ok(new
            {
                Message = "Login successful",
                CustomerId = customer.Id,
                customer.Username,
                customer.Name
            });
        }
    }
}
