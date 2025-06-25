using BusinessObjects;
using DataAccessLayer;
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
    public class ShoppingCartViewModel : BaseViewModel
    {
        public ObservableCollection<ProductForOrderViewModel> Cart { get; set; } = new();
        public ICommand PlaceOrderCommand { get; }

        private readonly OrderService _orderService = new();
        private readonly ProductService _productService = new();

        public ShoppingCartViewModel()
        {
            PlaceOrderCommand = new RelayCommand(_ => PlaceOrder(), _ => Cart.Any());
        }

        public void AddToCart(Product product, int quantity)
        {
            var existing = Cart.FirstOrDefault(p => p.Product.ProductID == product.ProductID);
            if (existing != null)
            {
                existing.Quantity += quantity;
                OnPropertyChanged(nameof(Cart));
            }
            else
            {
                Cart.Add(new ProductForOrderViewModel
                {
                    Product = product,
                    Quantity = quantity
                });
            }
        }

        public void ClearCart() => Cart.Clear();

        private void PlaceOrder()
        {
            if (SessionManager.CurrentCustomer == null)
            {
                MessageBox.Show("Bạn cần đăng nhập để đặt hàng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!Cart.Any())
            {
                MessageBox.Show("Giỏ hàng trống!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newOrder = new Order
            {
                CustomerID = SessionManager.CurrentCustomer.CustomerID,
                OrderDate = DateTime.Now,
                EmployeeID = new LucySalesDbContext().Employees.Select(e => e.EmployeeID).FirstOrDefault(),

                OrderDetails = Cart.Select(item => new OrderDetail
                {
                    ProductID = item.Product.ProductID,
                    UnitPrice = item.Product.UnitPrice ?? 0,
                    Quantity = (short)item.Quantity,
                    Discount = 0f
                }).ToList()
            };

            _orderService.Add(newOrder);
            ClearCart();

            MessageBox.Show("Đặt hàng thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
