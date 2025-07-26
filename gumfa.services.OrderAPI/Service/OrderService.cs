using gumfa.services.OrderAPI.Data;
using gumfa.services.OrderAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace gumfa.services.OrderAPI.Service
{
    public interface IOrderService
    {
        Task<List<Order>> getall();
        Task<Order> getbyid(int pkid);
        Task<Order> add(Order productDto);
        Task<Order> update(Order productDto);
        Task<Order> delete(int pkid);
    }

    public class OrderService : IOrderService
    {
        private readonly OrderDbContext _db;

        public OrderService(OrderDbContext db)
        {
            _db = db;
        }

        public async Task<List<Order>> getall()
        {
            return await _db.orders.ToListAsync();
        }

        public async Task<Order> getbyid(int pkid)
        {
            return await _db.orders.FirstAsync(u => u.OrderID == pkid);
        }

        public async Task<Order> add(Order order)
        {
            _db.orders.Add(order);
            await _db.SaveChangesAsync();
            return order;
        }

        public async Task<Order> update(Order order)
        {
            _db.orders.Update(order);
            await _db.SaveChangesAsync();
            return order;
        }

        public async Task<Order> delete(int pkid)
        {
            Order order = _db.orders.First(u => u.OrderID == pkid);
            _db.orders.Remove(order);
            await _db.SaveChangesAsync();
            return order;
        }
    }
}
