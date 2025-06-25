using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;

namespace Services
{
    public class EmployeeService
    {
        public readonly IEmployeeRepository _repo = EmployeeRepository.Instance;

        public IEnumerable<Employee> GetAll() => _repo.GetAll();
        public Employee? GetById(int id) => _repo.GetById(id);
        public Employee? Login(string username, string password) => _repo.Login(username, password);
    }
}
