using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NguyenHoangDungWPF.ViewModels
{
    public class OrderDetailForCustomerViewModel
    {
        public int ProductID { get; set; }
        public string ProductName => Product?.ProductName ?? "";
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public decimal Total => UnitPrice * Quantity * (1 - (decimal)Discount);

        public Product? Product { get; set; }
    }
}
