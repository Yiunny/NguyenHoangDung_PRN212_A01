using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private static CustomerRepository? _instance;
        private static readonly object _lock = new();
        private readonly LucySalesDbContext _context;

        private CustomerRepository()
        {
            _context = new LucySalesDbContext();
        }

        public static CustomerRepository Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ??= new CustomerRepository();
                }
            }
        }

        public IEnumerable<Customer> GetAll() => _context.Customers.OrderBy(c => c.CompanyName).ToList();

        public Customer? GetById(int id) => _context.Customers.FirstOrDefault(c => c.CustomerID == id);

        public IEnumerable<Customer> Search(string keyword) => _context.Customers
            .Where(c => c.CompanyName != null && c.CompanyName.Contains(keyword))
            .OrderBy(c => c.ContactName)
            .ToList();

        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void Update(Customer customer)
        {
            var existing = _context.Customers.FirstOrDefault(c => c.CustomerID == customer.CustomerID);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(customer);
                _context.SaveChanges();
            }

        }

        public void Delete(int id)
        {
            var customer = GetById(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
        }
    }
}
