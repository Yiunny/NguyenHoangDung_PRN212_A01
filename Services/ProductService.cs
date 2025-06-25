using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;

namespace Services
{
    public class ProductService
    {
        private readonly IProductRepository _repo = ProductRepository.Instance;

        public IEnumerable<Product> GetAll() => _repo.GetAll();
        public Product? GetById(int id) => _repo.GetById(id);
        public IEnumerable<Product> SearchByName(string keyword) => _repo.SearchByName(keyword);
        public IEnumerable<Product> GetByCategory(int categoryId) => _repo.GetByCategory(categoryId);

        public void Add(Product product) => _repo.Add(product);
        public void Update(Product product) => _repo.Update(product);
        public void Delete(int id) => _repo.Delete(id);
    }
}
