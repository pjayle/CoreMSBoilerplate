using gumfa.services.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace gumfa.services.OrderAPI.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Order> orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
