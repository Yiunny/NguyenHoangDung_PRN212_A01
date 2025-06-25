using NguyenHoangDungWPF.Utils;
using NguyenHoangDungWPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NguyenHoangDungWPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (SessionManager.IsEmployeeLoggedIn)
            {
                EmployeeMenu.Visibility = Visibility.Visible;
                CustomerMenu.Visibility = Visibility.Collapsed;

                WelcomeText.Text = $"Xin chào, {SessionManager.CurrentEmployee?.Name}!";
                Title = "Sales System - Nhân viên";

                MainFrame.Navigate(new CustomerPage());
            }
            else if (SessionManager.IsCustomerLoggedIn)
            {
                EmployeeMenu.Visibility = Visibility.Collapsed;
                CustomerMenu.Visibility = Visibility.Visible;

                WelcomeText.Text = $"Xin chào, {SessionManager.CurrentCustomer?.ContactName}!";
                Title = "Sales System - Khách hàng";

                MainFrame.Navigate(new ProfilePage());
            }
            else
            {
                MessageBox.Show("Không có người dùng đăng nhập!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }
        public void Navigate_Customer(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CustomerPage());
        }

        public void Navigate_Product(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProductPage());
        }

        public void Navigate_Order(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new OrderPage());
        }

        public void Navigate_Profile(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProfilePage());
        }
        public void Navigate_Report(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new OrderReportPage());
        }
        public void Logout_Click(object sender, RoutedEventArgs e)
        {
            SessionManager.Logout();
            var login = new LoginWindow();
            login.Show();
            this.Close();
        }
        private void OrderPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CustomerOrderPage());
        }
        private void ViewHistory_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CustomerOrderHistoryPage());
        }


    }
}
