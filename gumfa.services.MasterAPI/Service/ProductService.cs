using gumfa.services.MasterAPI.Data;
using gumfa.services.MasterAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace gumfa.services.MasterAPI.Service
{
    public interface IProductService
    {
        Task<List<Product>> getall();
        Task<Product> getbyid(int pkid);
        Task<Product> add(Product productDto);
        Task<Product> update(Product productDto);
        Task<Product> delete(int pkid);
    }

    public class ProductService : IProductService
    {
        private readonly MasterDbContext _db;

        public ProductService(MasterDbContext db)
        {
            _db = db;
        }

        public async Task<List<Product>> getall()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<Product> getbyid(int pkid)
        {
            return await _db.Products.FirstAsync(u => u.ProductID == pkid);
        }

        public async Task<Product> add(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<Product> update(Product product)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<Product> delete(int pkid)
        {
            Product product = _db.Products.First(u => u.ProductID == pkid);
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return product;
        }
    }
}
