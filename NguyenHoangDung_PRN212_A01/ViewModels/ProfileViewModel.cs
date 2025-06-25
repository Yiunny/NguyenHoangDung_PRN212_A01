using BusinessObjects;
using NguyenHoangDungWPF.Utils;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NguyenHoangDungWPF.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private readonly CustomerService _service = new();
        private Customer? _profile;

        public Customer? Profile
        {
            get => _profile;
            set { _profile = value; OnPropertyChanged(nameof(Profile)); }
        }

        public void LoadProfile()
        {
            if (SessionManager.CurrentCustomer != null)
            {
                Profile = _service.GetById(SessionManager.CurrentCustomer.CustomerID);
            }
        }

        public void UpdateProfile()
        {
            if (Profile != null)
                _service.Update(Profile);
        }
    }
}
