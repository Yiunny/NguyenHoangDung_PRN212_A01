using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderDetailService
    {
        private readonly IOrderDetailRepository _repo = OrderDetailRepository.Instance;

        public IEnumerable<OrderDetail> GetAll() => _repo.GetAll();

        public IEnumerable<OrderDetail> GetByOrderId(int orderId) => _repo.GetByOrderId(orderId);

        public void Add(OrderDetail detail) => _repo.Add(detail);

        public void Delete(int orderId, int productId) => _repo.Delete(orderId, productId);
    }
}
