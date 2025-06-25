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
            if (SessionManager.CurrentCustomer == null) return;

            var newOrder = new Order
            {
                CustomerID = SessionManager.CurrentCustomer.CustomerID,
                OrderDate = DateTime.Now,
                EmployeeID = 0, // không có nhân viên nào liên quan ở đây
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
        }
    }
}
