using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;

namespace Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _repo = CategoryRepository.Instance;

        public IEnumerable<Category> GetAll() => _repo.GetAll();
        public Category? GetById(int id) => _repo.GetById(id);
    }
}
