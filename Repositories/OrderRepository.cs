﻿using BusinessObjects;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private static OrderRepository? _instance;
        private static readonly object _lock = new();
        private readonly LucySalesDbContext _context;

        private OrderRepository()
        {
            _context = new LucySalesDbContext();
        }

        public static OrderRepository Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ??= new OrderRepository();
                }
            }
        }

        public IEnumerable<Order> GetAll() =>
            _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

        public IEnumerable<Order> GetByDateRange(DateTime start, DateTime end) =>
            _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Where(o => o.OrderDate >= start && o.OrderDate <= end)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

        public Order? GetById(int id) => _context.Orders.FirstOrDefault(o => o.OrderID == id);

        public void Add(Order order)
        {

            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var order = GetById(id);
            if (order != null)
            {
                // Xóa toàn bộ OrderDetail của order trước
                var details = _context.OrderDetails.Where(od => od.OrderID == id).ToList();
                _context.OrderDetails.RemoveRange(details);

                // Sau đó mới xóa order
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }

    }
}
