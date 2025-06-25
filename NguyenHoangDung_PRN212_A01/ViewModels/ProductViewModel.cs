using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using BusinessObjects;
using Services;
using System.Windows.Input;
using NguyenHoangDungWPF.Commands;

namespace NguyenHoangDungWPF.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        private readonly ProductService _service = new();
        public ObservableCollection<Product> Products { get; } = new();
        public ICommand LoadCommand { get; }

        public ProductViewModel()
        {
            LoadCommand = new RelayCommand(_ => Load());
        }

        private void Load()
        {
            Products.Clear();
            foreach (var p in _service.GetAll())
            {
                Products.Add(p);
            }
        }
    }
}
