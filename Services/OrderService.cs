using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;

namespace Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepo = OrderRepository.Instance;
        private readonly IOrderDetailRepository _detailRepo = OrderDetailRepository.Instance;


        public IEnumerable<Order> GetAll() => _orderRepo.GetAll();
        public IEnumerable<Order> GetByDateRange(DateTime start, DateTime end) => _orderRepo.GetByDateRange(start, end);
        public Order? GetById(int id) => _orderRepo.GetById(id);
        public void Add(Order order) => _orderRepo.Add(order);
        public void Update(Order order) => _orderRepo.Update(order);
        public void Delete(int id) => _orderRepo.Delete(id);


        public IEnumerable<OrderDetail> GetDetails(int orderId)
        {
            var details = _detailRepo.GetAll().Where(d => d.OrderID == orderId).ToList();
            Console.WriteLine($"OrderService: {details.Count} bản ghi từ repo");
            return details;
        }
        public void AddDetail(OrderDetail detail) => _detailRepo.Add(detail);
        public void DeleteDetail(int orderId, int productId) => _detailRepo.Delete(orderId, productId);
    }
}
