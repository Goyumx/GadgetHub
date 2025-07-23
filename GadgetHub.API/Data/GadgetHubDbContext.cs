using Microsoft.EntityFrameworkCore;
using GadgetHub.API.Models;

namespace GadgetHub.API.Data
{
    public class GadgetHubDbContext : DbContext
    {
        public GadgetHubDbContext(DbContextOptions<GadgetHubDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
