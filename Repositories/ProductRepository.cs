using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        private static ProductRepository? _instance;
        private static readonly object _lock = new();
        private readonly LucySalesDbContext _context;

        private ProductRepository()
        {
            _context = new LucySalesDbContext();
        }

        public static ProductRepository Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ??= new ProductRepository();
                }
            }
        }

        public IEnumerable<Product> GetAll() => _context.Products
            .Where(p => !p.Discontinued)
            .OrderBy(p => p.ProductName)
            .ToList();

        public Product? GetById(int id) => _context.Products.FirstOrDefault(p => p.ProductID == id);

        public IEnumerable<Product> SearchByName(string keyword) => _context.Products
            .Where(p => p.ProductName.Contains(keyword) && !p.Discontinued)
            .OrderByDescending(p => p.UnitsInStock)
            .ToList();
        public IEnumerable<Product> GetByCategory(int categoryId)
        {
            return _context.Products.Where(p => p.CategoryID == categoryId).ToList();
        }


        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            var existing = _context.Products.FirstOrDefault(p => p.ProductID == product.ProductID);
            if (existing != null)
            {
                existing.ProductName = product.ProductName;
                existing.UnitPrice = product.UnitPrice;
                existing.UnitsInStock = product.UnitsInStock;
                _context.SaveChanges();
            }
        }


        public void Delete(int id)
        {
            var product = GetById(id);
            if (product == null) return;

            // Kiểm tra sản phẩm đã được đặt hàng chưa
            bool isReferenced = _context.OrderDetails.Any(od => od.ProductID == id);
            if (isReferenced)
            {
                throw new InvalidOperationException("Không thể xóa sản phẩm vì đã tồn tại trong đơn hàng.");
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
        }



    }
}
