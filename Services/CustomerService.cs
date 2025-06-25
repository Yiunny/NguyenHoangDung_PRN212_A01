using BusinessObjects;
using Repositories;

namespace Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _repo = CustomerRepository.Instance;

        public IEnumerable<Customer> GetAll() => _repo.GetAll();
        public Customer? GetById(int id) => _repo.GetById(id);
        public static IEnumerable<Customer> Search(string keyword) => CustomerRepository.Instance.Search(keyword);
        public void Add(Customer customer) => _repo.Add(customer);
        public void Update(Customer customer) => _repo.Update(customer);
        public void Delete(int id) => _repo.Delete(id);
    }
}
