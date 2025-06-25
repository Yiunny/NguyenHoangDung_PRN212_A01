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
using System.Windows.Navigation;
using System.Windows.Shapes;
using NguyenHoangDungWPF.ViewModels;

namespace NguyenHoangDungWPF.Views
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            ((ProfileViewModel)DataContext).LoadProfile();
        }
        public void Update_Click(object sender, RoutedEventArgs e)
        {
            var vm = (ProfileViewModel)DataContext;
            vm.UpdateProfile();
            MessageBox.Show("Đã cập nhật hồ sơ!", "Thành công");
        }

    }
}
