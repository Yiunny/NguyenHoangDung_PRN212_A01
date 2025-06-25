using BusinessObjects;
using NguyenHoangDungWPF.Commands;
using NguyenHoangDungWPF.Views;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NguyenHoangDungWPF.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        private readonly ProductService _service = new();
        public ObservableCollection<Product> Products { get; } = new();
        public ICommand LoadCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }


        public ProductViewModel()
        {
            LoadCommand = new RelayCommand(_ => Load());
            AddCommand = new RelayCommand(_ => Add());
            EditCommand = new RelayCommand(p => Edit(p as Product));
            DeleteCommand = new RelayCommand(p => Delete(p as Product));
        }

        private void Load()
        {
            Products.Clear();
            foreach (var p in _service.GetAll())
            {
                Products.Add(p);
            }
        }
        private void Add()
        {
            var dialog = new ProductDialog(new Product());
            if (dialog.ShowDialog() == true)
            {
                _service.Add(dialog.ProductData);
                Load();
            }
        }

        private void Edit(Product? product)
        {
            if (product == null) return;
            var clone = new Product
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock
            };
            var dialog = new ProductDialog(clone);
            if (dialog.ShowDialog() == true)
            {
                _service.Update(dialog.ProductData);
                Load();
            }
        }

        private void Delete(Product? product)
        {
            if (product == null) return;

            if (MessageBox.Show("Xoá sản phẩm này?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    _service.Delete(product.ProductID);
                    Load();
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa sản phẩm: " + ex.Message);
                }
            }
        }

    }
}
