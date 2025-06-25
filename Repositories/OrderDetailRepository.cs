using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private static OrderDetailRepository? _instance;
        private static readonly object _lock = new();

        private readonly LucySalesDbContext _context;

        private OrderDetailRepository()
        {
            _context = new LucySalesDbContext();
        }

        public static OrderDetailRepository Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ??= new OrderDetailRepository();
                }
            }
        }

        public IEnumerable<OrderDetail> GetByOrderId(int orderId) =>
            _context.OrderDetails.Where(od => od.OrderID == orderId).ToList();

        public void Add(OrderDetail detail)
        {
            _context.OrderDetails.Add(detail);
            _context.SaveChanges();
        }

        public void Delete(int orderId, int productId)
        {
            var od = _context.OrderDetails.FirstOrDefault(od => od.OrderID == orderId && od.ProductID == productId);
            if (od != null)
            {
                _context.OrderDetails.Remove(od);
                _context.SaveChanges();
            }
        }
        public IEnumerable<OrderDetail> GetAll()
        {
            var list = _context.OrderDetails.ToList();

            foreach (var item in list)
            {
                Console.WriteLine($"DEBUG: OrderID = {item.OrderID}, ProductID = {item.ProductID}, Qty = {item.Quantity}");
            }

            return list;
        }

    }
}
