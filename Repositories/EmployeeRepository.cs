using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private static EmployeeRepository? _instance;
        private static readonly object _lock = new();

        private readonly LucySalesDbContext _context;

        private EmployeeRepository()
        {
            _context = new LucySalesDbContext();
        }

        public static EmployeeRepository Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ??= new EmployeeRepository();
                }
            }
        }

        public IEnumerable<Employee> GetAll() => _context.Employees.ToList();

        public Employee? GetById(int id) => _context.Employees.FirstOrDefault(e => e.EmployeeID == id);

        public Employee? Login(string username, string password) =>
            _context.Employees.FirstOrDefault(e => e.UserName == username && e.Password == password);
    }
}
