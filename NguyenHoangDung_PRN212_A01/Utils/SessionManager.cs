using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NguyenHoangDungWPF.Utils
{
    internal class SessionManager
    {
        public static Employee? CurrentEmployee { get; set; }
        public static Customer? CurrentCustomer { get; set; }

        public static bool IsEmployeeLoggedIn => CurrentEmployee != null;
        public static bool IsCustomerLoggedIn => CurrentCustomer != null;

        public static void Logout()
        {
            CurrentEmployee = null;
            CurrentCustomer = null;
        }
    }
}
