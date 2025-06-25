using BusinessObjects;
using NguyenHoangDungWPF.Commands;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace NguyenHoangDungWPF.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        private readonly OrderService _service = new();
        private Order _selectedOrder;

        public ObservableCollection<Order> Orders { get; } = new();
        public ObservableCollection<OrderDetail> Details { get; } = new();

        public ICommand LoadCommand { get; }
        public ICommand OrderSelectedCommand { get; }

        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged(nameof(SelectedOrder));
                if (_selectedOrder != null)
                {
                    LoadDetails(_selectedOrder.OrderID);
                }
            }
        }

        public OrderViewModel()
        {
            LoadCommand = new RelayCommand(_ => Load());
            OrderSelectedCommand = new RelayCommand(OnOrderSelected);
        }

        private void OnOrderSelected(object obj)
        {
            if (obj is Order order)
            {
                LoadDetails(order.OrderID);
            }
        }

        private void Load()
        {
            try
            {
                Orders.Clear();
                var list = _service.GetAll();
                foreach (var o in list)
                {
                    Orders.Add(o);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading orders: {ex.Message}");
            }
        }

        public void LoadDetails(int orderId)
        {
            try
            {
                Details.Clear();
                var list = _service.GetDetails(orderId).ToList();
                foreach (var d in list)
                {
                    Details.Add(d);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading details: {ex.Message}");
            }
        }
    }
}