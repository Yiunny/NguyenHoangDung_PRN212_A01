using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NguyenHoangDungWPF.ViewModels
{
    public class ProductForOrderViewModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public decimal Total => (Product.UnitPrice ?? 0) * Quantity;

    }
}
