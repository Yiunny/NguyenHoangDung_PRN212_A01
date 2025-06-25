using BusinessObjects;
using NguyenHoangDungWPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace NguyenHoangDungWPF.Views
{
    public partial class OrderPage : Page
    {
        public OrderPage()
        {
            InitializeComponent();
            this.DataContext = new OrderViewModel();
            if (DataContext == null)
                MessageBox.Show("DataContext is NULL");
        }

        private void OrderSelected(object sender, SelectionChangedEventArgs e)
        {
            var vm = (OrderViewModel)DataContext;

            if (e.AddedItems.Count > 0 && e.AddedItems[0] is Order selectedOrder)
            {
                vm.LoadDetails(selectedOrder.OrderID);
            }
        }
    }
}
