using BusinessObjects;
using NguyenHoangDungWPF.Commands;
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
    public class CustomerOrderViewModel : BaseViewModel
    {
        private readonly ProductService _productService = new();
        private readonly CategoryService _categoryService = new();

        public ObservableCollection<Category> Categories { get; set; } = new();
        public ObservableCollection<Product> Products { get; set; } = new();
        private Product? _selectedProduct;
        public Product? SelectedProduct
        {
            get => _selectedProduct;
            set { _selectedProduct = value; OnPropertyChanged(nameof(SelectedProduct)); }
        }
        private int _quantityToAdd = 1;
        public int QuantityToAdd
        {
            get => _quantityToAdd;
            set { _quantityToAdd = value; OnPropertyChanged(nameof(QuantityToAdd)); }
        }
        public ICommand AddToCartCommand { get; }

        public CustomerOrderViewModel(ICommand addToCartCommand)
        {
            AddToCartCommand = new RelayCommand(_ => AddToCartExecute(), _ => SelectedProduct != null && QuantityToAdd > 0);

        }

        private Category? _selectedCategory;
        public Category? SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
                LoadProducts();
            }
        }

        public ShoppingCartViewModel CartViewModel { get; set; } = new();

        public CustomerOrderViewModel()
        {
            Categories = new ObservableCollection<Category>(_categoryService.GetAll());
        }

        public void LoadProducts()
        {
            Products.Clear();
            if (SelectedCategory != null)
            {
                var list = _productService.GetByCategory(SelectedCategory.CategoryID);
                foreach (var item in list)
                    Products.Add(item);
            }
        }

        public void AddToCart(Product product, int quantity)
        {
            CartViewModel.AddToCart(product, quantity);
        }
        private void AddToCartExecute()
        {
            if (SelectedProduct != null && QuantityToAdd > 0)
            {
                CartViewModel.AddToCart(SelectedProduct, QuantityToAdd);
            }
        }

    }
}
