using gumfa.services.ProductAPICQRS.Data;
using gumfa.services.ProductAPICQRS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace gumfa.services.ProductAPICQRS.Service
{
    public interface IEmployeeService
    {
        Task<List<Employee>> getall();
        Task<Employee> getbyid(int pkid);
        Task<Employee> add(Employee employee);
        Task<Employee> update(Employee employee);
        Task<Employee> delete(int pkid);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeDbContext _db;

        public EmployeeService(EmployeeDbContext db)
        {
            _db = db;
        }

        public async Task<List<Employee>> getall()
        {
            return await _db.employees.ToListAsync();
        }

        public async Task<Employee> getbyid(int pkid)
        {
            return await _db.employees.FirstAsync(u => u.EmployeeID == pkid);
        }

        public async Task<Employee> add(Employee product)
        {
            var result = _db.employees.Add(product);
            await _db.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Employee> update(Employee product)
        {
            var result = _db.employees.Update(product);
            await _db.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Employee> delete(int pkid)
        {
            Employee product = _db.employees.First(u => u.EmployeeID == pkid);
            _db.employees.Remove(product);
            await _db.SaveChangesAsync();
            return product;
        }
    }
}
