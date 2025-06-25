using BusinessObjects;
using NguyenHoangDungWPF.Commands;
using NguyenHoangDungWPF.Utils;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NguyenHoangDungWPF.ViewModels
{
    public class CustomerOrderHistoryViewModel : BaseViewModel
    {
        private readonly OrderService _orderService = new();
        private readonly OrderDetailService _detailService = new();

        public ObservableCollection<Order> Orders { get; } = new();
        public ObservableCollection<OrderDetailForCustomerViewModel> Details { get; } = new();

        private Order? _selectedOrder;
        public Order? SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged(nameof(SelectedOrder));
                LoadDetails();
            }
        }

        public ICommand CancelOrderCommand { get; }

        public CustomerOrderHistoryViewModel()
        {
            CancelOrderCommand = new RelayCommand(_ => CancelOrder(), _ => SelectedOrder != null);
            LoadOrders();
        }

        private void LoadOrders()
        {
            Orders.Clear();
            if (SessionManager.CurrentCustomer != null)
            {
                var list = _orderService.GetAll()
                    .Where(o => o.CustomerID == SessionManager.CurrentCustomer.CustomerID)
                    .OrderByDescending(o => o.OrderDate);
                foreach (var order in list)
                    Orders.Add(order);
            }
        }

        private void LoadDetails()
        {
            Details.Clear();
            if (SelectedOrder == null) return;

            var details = _detailService.GetByOrderId(SelectedOrder.OrderID);
            foreach (var d in details)
            {
                Details.Add(new OrderDetailForCustomerViewModel
                {
                    ProductID = d.ProductID,
                    Product = d.Product,
                    UnitPrice = d.UnitPrice,
                    Quantity = d.Quantity,
                    Discount = d.Discount
                });
            }
        }

        private void CancelOrder()
        {
            if (SelectedOrder == null) return;

            if (MessageBox.Show("Bạn có chắc chắn muốn hủy đơn hàng này không?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _orderService.Delete(SelectedOrder.OrderID);
                LoadOrders();
                Details.Clear();
                MessageBox.Show("Đã hủy đơn hàng.");
            }
        }
    }
}
