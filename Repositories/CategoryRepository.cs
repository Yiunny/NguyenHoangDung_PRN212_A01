using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private static CategoryRepository? _instance;
        private static readonly object _lock = new();

        private readonly LucySalesDbContext _context;

        private CategoryRepository()
        {
            _context = new LucySalesDbContext();
        }

        public static CategoryRepository Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ??= new CategoryRepository();
                }
            }
        }

        public IEnumerable<Category> GetAll() => _context.Categories.ToList();

        public Category? GetById(int id) => _context.Categories.FirstOrDefault(c => c.CategoryID == id);
    }
}
