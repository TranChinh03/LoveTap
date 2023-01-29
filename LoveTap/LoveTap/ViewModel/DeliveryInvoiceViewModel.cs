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
    public class DeliveryInvoiceViewModel: BaseViewModel
    {
        public ICommand navBack { get; set; }

        private string _TypeOrder;
        public string TypeOrder { get => _TypeOrder; set { _TypeOrder = value; OnPropertyChanged(); } }
        private string _TextTest;
        public string TextTest { get => _TextTest; set { _TextTest = value; OnPropertyChanged(); } }

        private int _ID;
        public int ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }

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


        private string _BranchAddress;
        public string BranchAddress { get => _BranchAddress; set { _BranchAddress = value; OnPropertyChanged(); } }

        private string _BranchEmail;
        public string BranchEmail { get => _BranchEmail; set { _BranchEmail = value; OnPropertyChanged(); } }


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

        private ObservableCollection<CHINHANH> _BranchList;
        public ObservableCollection<CHINHANH> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged(); } }

        public struct Order
        {
            public int Stt { get; set; }
            public string Ten { get; set; }
            public double Gia { get; set; }
            public int SoLuong { get; set; }
            public double TongTien { get; set; }
        }

        private List<Order> _MyOrderList = new List<Order>();
        public List<Order> MyOrderList { get => _MyOrderList; set { _MyOrderList = value; } }

        public DeliveryInvoiceViewModel(NavigationStore navigationStore)
        {
            navBack = new NavigationCommand<OrderDetailViewModel>(navigationStore, () => new OrderDetailViewModel(navigationStore));

            ProductList = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs);
            OrderDetailList = new ObservableCollection<CTHD>(DataProvider.Ins.DB.CTHDs);
            CustomerList = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
            EmployeeList = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs);
            BranchList = new ObservableCollection<CHINHANH>(DataProvider.Ins.DB.CHINHANHs);

            OrdersViewModel.Order temp = OrdersViewModel.OrderSelected;

            ID = (int)temp.ID;
            Date = (DateTime)temp.Date;
            SubTotal = 0;
            
            foreach (NHANVIEN nv in DataProvider.Ins.DB.NHANVIENs.Where(x => x.MANV == temp.MANV))
            {
                EmployeeName = nv.HOTEN.ToString();
                EmployeeID = nv.MANV.ToString();
                Branch = nv.MACN.ToString();
            }

            foreach (CHINHANH cn in DataProvider.Ins.DB.CHINHANHs.Where(x => x.MACN.ToString() == Branch))
            {
                BranchAddress = cn.DIACHI;
                BranchEmail = cn.EMAIL.ToString();
            }

            foreach (KHACHHANG kh in DataProvider.Ins.DB.KHACHHANGs.Where(x => x.MAKH == temp.MAKH))
            {
                CustomerName = kh.HOTEN.ToString();
                Phone = kh.SDT.ToString();
                Address = kh.DIACHI.ToString();
            }
            int stt = 0;

            for (int i = 0; i < DataProvider.Ins.DB.CTHDs.Count(); i++)
            {
                if (OrderDetailList[i].MAHD == ID)
                {
                    Order order = new Order();
                    for (int j = 0; j < DataProvider.Ins.DB.SANPHAMs.Count(); j++)
                    {
                        if (ProductList[j].MASP == OrderDetailList[i].MASP)
                        {
                            stt++;
                            order.Stt = stt;
                            order.Ten = ProductList[j].TEN;
                            order.Gia = (double)ProductList[j].GIA;
                            order.SoLuong = (int)OrderDetailList[i].SOLUONG;
                            order.TongTien = order.SoLuong * order.Gia;
                            SubTotal += order.TongTien;
                            MyOrderList.Add(order);
                        }

                    }

                }
            }
        }
    }
}
