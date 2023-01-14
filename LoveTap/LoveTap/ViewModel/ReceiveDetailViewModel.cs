using LoveTap.Commands;
using LoveTap.Model;
using LoveTap.Stores;
using LoveTap.UserControlCustom;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace LoveTap.ViewModel
{
    public class ReceiveDetailViewModel:BaseViewModel
    {
        public ICommand navBack { get; set; }

        private string _TypeOrder;
        public string TypeOrder { get => _TypeOrder; set { _TypeOrder = value; OnPropertyChanged(); } }
        private string _TextTest;
        public string TextTest { get => _TextTest; set { _TextTest = value; OnPropertyChanged(); } }

        private string _ID;
        public string ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }

        private DateTime _Date;
        public DateTime Date { get => _Date; set { _Date = value; OnPropertyChanged(); } }

        private double _SubTotal;
        public double SubTotal { get => _SubTotal; set { _SubTotal = value; OnPropertyChanged(); } }

        private string _CustomerName;
        public string CustomerName { get => _CustomerName; set { _CustomerName = value; OnPropertyChanged(); } }


        private string _Phone;
        public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }


        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }


        private string _EmployeeName;
        public string EmployeeName { get => _EmployeeName; set { _EmployeeName = value; OnPropertyChanged(); } }

        private string _EmployeeID;
        public string EmployeeID { get => _EmployeeID; set { _EmployeeID = value; OnPropertyChanged(); } }

        private string _Branch;
        public string Branch { get => _Branch; set { _Branch = value; OnPropertyChanged(); } }

        private ObservableCollection<NHANVIEN> _EmployeeList;
        public ObservableCollection<NHANVIEN> EmployeeList { get => _EmployeeList; set { _EmployeeList = value; OnPropertyChanged(); } }

        private ObservableCollection<KHACHHANG> _CustomerList;
        public ObservableCollection<KHACHHANG> CustomerList { get => _CustomerList; set { _CustomerList = value; OnPropertyChanged(); } }

        private ObservableCollection<HOADON> _OrderList;
        public ObservableCollection<HOADON> OrderList { get => _OrderList; set { _OrderList = value; OnPropertyChanged(); } }

        private ObservableCollection<CTHD> _OrderDetailList;
        public ObservableCollection<CTHD> OrderDetailList { get => _OrderDetailList; set { _OrderDetailList = value; OnPropertyChanged(); } }

        private ObservableCollection<SANPHAM> _ProductList;
        public ObservableCollection<SANPHAM> ProductList { get => _ProductList; set { _ProductList = value; OnPropertyChanged(); } }

        public struct Order
        {
            public string Ten { get; set; }
            public double Gia { get; set; }
            public int SoLuong { get; set; }
            public double TongTien { get; set; }
        }

        private List<Order> _MyOrderList = new List<Order>();
        public List<Order> MyOrderList { get => _MyOrderList; set { _MyOrderList = value; } }
        public ICommand LoadedOrderDetail { get; set; }
        string Name;
        public ReceiveDetailViewModel( NavigationStore navigationStore)
        {
            navBack = new NavigationCommand<OrdersViewModel>(navigationStore, () => new OrdersViewModel(navigationStore));
        }
    }
}
