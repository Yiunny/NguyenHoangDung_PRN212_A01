using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using NguyenHoangDungWPF.Commands;
using Services;
using System.Windows.Input;

namespace NguyenHoangDungWPF.ViewModels
{
    internal class OrderReportViewModel : BaseViewModel
    {
        private readonly OrderService _service = new();

        public ObservableCollection<Order> Orders { get; } = new();

        public DateTime StartDate { get; set; } = DateTime.Today.AddMonths(-1);
        public DateTime EndDate { get; set; } = DateTime.Today;

        private string _summary = "";
        public string Summary
        {
            get => _summary;
            set { _summary = value; OnPropertyChanged(nameof(Summary)); }
        }

        public ICommand ReportCommand { get; }

        public OrderReportViewModel()
        {
            ReportCommand = new RelayCommand(_ => GenerateReport());
        }

        private void GenerateReport()
        {
            var result = _service.GetByDateRange(StartDate, EndDate);
            Orders.Clear();
            foreach (var order in result)
            {
                Orders.Add(order);
            }

            Summary = $"Tổng đơn hàng: {Orders.Count}";
        }
    }
}
