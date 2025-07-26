using gumfa.services.ProductAPICQRS.Models;
using gumfa.services.ProductAPICQRS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace gumfa.services.ProductAPICQRS.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Employee> employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Product
            var difficulties = new List<Employee>()
            {
                new Employee()
                {
                    EmployeeID = 1,
                    EMPCode = "EMP001",
                    Name = "Employee A",
                    IsActive=true
                },
                new Employee()
                {
                    EmployeeID = 2,
                    EMPCode = "EMP002",
                    Name = "Employee B",
                    IsActive=true
                }
            };
           // modelBuilder.Entity<Product>().Property(p => p.MRP).HasPrecision(18, 4);

            // Seed difficulties to the database
            modelBuilder.Entity<Employee>().HasData(difficulties);
        }
    }
}
