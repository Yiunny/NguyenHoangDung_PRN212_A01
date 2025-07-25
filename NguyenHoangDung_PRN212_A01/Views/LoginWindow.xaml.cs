﻿using System;
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
using NguyenHoangDung_PRN212_A01;
using NguyenHoangDungWPF.ViewModels;


namespace NguyenHoangDungWPF.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        public void Login_Click(object sender, RoutedEventArgs e)
        {
            var vm = (LoginViewModel)DataContext;
            vm.Password = PasswordBox.Password;

            vm.Login(); // ✅ GỌI ĐĂNG NHẬP TRƯỚC KHI KIỂM TRA LoggedInUser

            if (vm.LoggedInUser != null)
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                var mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid credentials.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LoginEmployee_Click(object sender, RoutedEventArgs e)
        {
            var vm = (LoginViewModel)DataContext;
            vm.Password = PasswordBox.Password;
            vm.Login();

            if (vm.LoggedInUser != null)
            {
                var main = new MainWindow();
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid employee credentials.", "Login Failed");
            }
        }

        private void LoginCustomer_Click(object sender, RoutedEventArgs e)
        {
            var vm = (LoginViewModel)DataContext;
            vm.Password = PasswordBox.Password;
            vm.LoginAsCustomer();

            if (vm.LoggedInCustomer != null)
            {
                var main = new MainWindow();
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid customer credentials.", "Login Failed");
            }
        }
        private void TogglePassword_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Visibility == Visibility.Visible)
            {
                VisiblePasswordBox.Text = PasswordBox.Password;
                PasswordBox.Visibility = Visibility.Collapsed;
                VisiblePasswordBox.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordBox.Password = VisiblePasswordBox.Text;
                PasswordBox.Visibility = Visibility.Visible;
                VisiblePasswordBox.Visibility = Visibility.Collapsed;
            }
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (VisiblePasswordBox.Visibility == Visibility.Visible)
            {
                VisiblePasswordBox.Text = PasswordBox.Password;
            }
        }

        private void VisiblePasswordBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PasswordBox.Visibility == Visibility.Visible)
            {
                PasswordBox.Password = VisiblePasswordBox.Text;
            }
        }

        private string GetCurrentPassword()
        {
            return PasswordBox.Visibility == Visibility.Visible
                ? PasswordBox.Password
                : VisiblePasswordBox.Text;
        }




        public void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
