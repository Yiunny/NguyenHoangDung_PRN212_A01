using BusinessObjects;
using NguyenHoangDungWPF.Commands;
using NguyenHoangDungWPF.Utils;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NguyenHoangDungWPF.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public string _username = "";
        public string _password = "";
        public Employee? _loggedInUser;

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        public Employee? LoggedInUser
        {
            get => _loggedInUser;
            set { _loggedInUser = value; OnPropertyChanged(nameof(LoggedInUser)); }
        }

        public ICommand LoginCommand { get; }

        public readonly EmployeeService _employeeService = new();
        public Customer? LoggedInCustomer { get; set; }

        public ICommand LoginCustomerCommand { get; }

        public readonly CustomerService _customerService = new();

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(_ => Login());
            LoginCustomerCommand = new RelayCommand(_ => LoginAsCustomer());
        }

        public void Login()
        {
            LoggedInUser = _employeeService.Login(Username, Password);
            if (LoggedInUser != null)
                SessionManager.CurrentEmployee = LoggedInUser;
        }
        public void LoginAsCustomer()
        {
            var customers = _customerService.GetAll();
            LoggedInCustomer = customers.FirstOrDefault(c =>
                c.ContactName?.Trim().ToLower() == Username.Trim().ToLower() &&
                c.Phone?.Trim() == Password.Trim());


            if (LoggedInCustomer != null)
                SessionManager.CurrentCustomer = LoggedInCustomer;
        }

    }
}
