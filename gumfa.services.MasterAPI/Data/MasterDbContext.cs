using gumfa.services.MasterAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace gumfa.services.MasterAPI.Data
{
    public class MasterDbContext : DbContext
    {
        public MasterDbContext(DbContextOptions<MasterDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Product
            var difficulties = new List<Product>()
            {
                new Product()
                {
                    ProductID = 1,
                    ProductName = "Product 001",
                    Description = "Product Decription 001",
                    MRP= 100,
                    IsActive=true
                },
                new Product()
                {
                    ProductID = 2,
                    ProductName = "Product 002",
                    Description = "Product Decription 002",
                    MRP= 100,
                    IsActive=true
                }
            };
            modelBuilder.Entity<Product>().Property(p => p.MRP).HasPrecision(18, 4);
            
            // Seed difficulties to the database
            modelBuilder.Entity<Product>().HasData(difficulties);
        }
    }
}
