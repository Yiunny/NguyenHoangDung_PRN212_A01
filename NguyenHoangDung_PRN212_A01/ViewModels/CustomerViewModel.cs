using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Services;
using System.Windows.Input;
using NguyenHoangDungWPF.Commands;
using System.Windows;
using NguyenHoangDungWPF.Views;

namespace NguyenHoangDungWPF.ViewModels
{
    public class CustomerViewModel : BaseViewModel
    {
        public readonly CustomerService _service = new();
        public ObservableCollection<Customer> Customers { get; } = new();
        public ICommand LoadCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public CustomerViewModel()
        {
            LoadCommand = new RelayCommand(_ => Load());
            AddCommand = new RelayCommand(_ => AddCustomer());
            EditCommand = new RelayCommand(param => EditCustomer(param as Customer));
            DeleteCommand = new RelayCommand(param => DeleteCustomer(param as Customer));
        }

        private void Load()
        {
            Customers.Clear();
            foreach (var c in _service.GetAll())
            {
                Customers.Add(c);
            }
        }
        private void AddCustomer()
        {
            var newCustomer = new Customer();
            var dialog = new CustomerDialog(newCustomer);
            if (dialog.ShowDialog() == true)
            {
                _service.Add(dialog.CustomerData);
                Load();
            }
        }

        private void EditCustomer(Customer? customer)
        {
            if (customer == null) return;
            var clone = new Customer
            {
                CustomerID = customer.CustomerID,
                CompanyName = customer.CompanyName,
                ContactName = customer.ContactName,
                Phone = customer.Phone
            };
            var dialog = new CustomerDialog(clone);
            if (dialog.ShowDialog() == true)
            {
                _service.Update(dialog.CustomerData);
                Load();
            }
        }

        private void DeleteCustomer(Customer? customer)
        {
            if (customer == null) return;
            if (MessageBox.Show("Bạn có chắc muốn xóa khách hàng này?", "Xác nhận",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _service.Delete(customer.CustomerID);
                Load();
            }
        }
        private Customer? _selectedCustomer;
        public Customer? SelectedCustomer
        {
            get => _selectedCustomer;
            set { _selectedCustomer = value; OnPropertyChanged(nameof(SelectedCustomer)); }
        }

    }
}
