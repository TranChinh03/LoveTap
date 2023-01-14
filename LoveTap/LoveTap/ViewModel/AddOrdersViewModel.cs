using LoveTap.Model;
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
using System.Windows.Navigation;
using LoveTap.Stores;
using LoveTap.Commands;

namespace LoveTap.ViewModel
{
    public class AddOrdersViewModel : BaseViewModel
    {
        public ICommand navBack { get; set; }
        public ICommand navDone { get; set; }

        public ICommand AddCommand { get; set; }

        public ICommand DoneCommand { get; set; }

        private int _OrderID;
        public int OrderID { get => _OrderID; set { _OrderID = value; OnPropertyChanged(); } }

        private DateTime _Date;
        public DateTime Date { get => _Date; set { _Date = value; OnPropertyChanged(); } }

        private string _Type;
        public string Type { get => _Type; set { _Type = value; OnPropertyChanged(); } }

        private int _GoodID;
        public int GoodID { get => _GoodID; set { _GoodID = value; OnPropertyChanged(); } }


        private string _Amount;
        public string Amount { get => _Amount; set { _Amount = value; OnPropertyChanged(); } }



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

        private int _Branch;
        public int Branch { get => _Branch; set { _Branch = value; OnPropertyChanged(); } }

        private ObservableCollection<HOADON> _OrdersList;
        public ObservableCollection<HOADON> OrdersList { get => _OrdersList; set { _OrdersList = value; OnPropertyChanged(); } }

        private ObservableCollection<CTHD> _OrderDetailList;
        public ObservableCollection<CTHD> OrderDetailList { get => _OrderDetailList; set { _OrderDetailList = value; OnPropertyChanged(); } }

        private ObservableCollection<SANPHAM> _ProductList;
        public ObservableCollection<SANPHAM> ProductList { get => _ProductList; set { _ProductList = value; OnPropertyChanged(); } }

        private ObservableCollection<NHANVIEN> _EmployeeList;
        public ObservableCollection<NHANVIEN> EmployeeList { get => _EmployeeList; set { _EmployeeList = value; OnPropertyChanged(); } }

        private ObservableCollection<KHACHHANG> _CustomerList;
        public ObservableCollection<KHACHHANG> CustomerList { get => _CustomerList; set { _CustomerList = value; OnPropertyChanged(); } }



        //public struct Detail
        //{
        //    public string ten { get; set; }
        //    public string sl { get; set; }
        //}

        private List<CTHD> _MyOrderDetailList = new List<CTHD>();
        public List<CTHD> MyOrderDetailList { get { return _MyOrderDetailList; } set { _MyOrderDetailList = value; OnPropertyChanged(); } }
        public AddOrdersViewModel(NavigationStore navigationStore)
        {

            OrdersList = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            OrderDetailList = new ObservableCollection<CTHD>(DataProvider.Ins.DB.CTHDs);
            ProductList = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs);
            CustomerList = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
            EmployeeList = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs);
            SubTotal = 0;
            Date = DateTime.Now;

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (  string.IsNullOrEmpty(GoodID.ToString()) || string.IsNullOrEmpty(Amount))
                    return false;
                var orderDetailList = DataProvider.Ins.DB.CTHDs.Where(x => x.MAHD == OrderID && x.MASP == GoodID);
                if (orderDetailList == null || orderDetailList.Count() != 0)
                    return false;
                return true;
            }, (p) =>
            {
                var cthd = new CTHD();
                cthd.MAHD = OrderID;
                cthd.MASP = GoodID;
                cthd.SOLUONG = int.Parse(Amount);
                MyOrderDetailList.Add(cthd);
                foreach(SANPHAM sp in ProductList)
                {
                    if (sp.MASP == cthd.MASP)
                        SubTotal += (double)(cthd.SOLUONG * sp.GIA);
                }    
            });


            DoneCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(EmployeeName))
                    return false;
                var orderList = DataProvider.Ins.DB.HOADONs.Where(x => x.MAHD == OrderID);
                if (orderList == null || orderList.Count() != 0)
                    return false;
                return true;
            }, (p) =>
            {
                var hd = new HOADON();
                hd.MAHD = OrderID;
                hd.NGMUA = Date;
                hd.TONGTIEN = 0;

                foreach (NHANVIEN nv in EmployeeList)
                    if (nv.HOTEN == EmployeeName)
                        hd.MANV = nv.MANV;

                //hd.SDT = Phone;

                foreach (KHACHHANG kh in CustomerList)
                    if (kh.SDT == Phone)
                    {
                        hd.MAKH = kh.MAKH;
                        kh.DOANHSO += SubTotal;
                    }
                hd.DELETED = false;
                DataProvider.Ins.DB.HOADONs.Add(hd);
                DataProvider.Ins.DB.SaveChanges();
                foreach (CTHD cthd in MyOrderDetailList)
                {

                    DataProvider.Ins.DB.CTHDs.Add(cthd);
                    DataProvider.Ins.DB.SaveChanges();
                }

            });


            navBack = new NavigationCommand<OrdersViewModel>(navigationStore, () => new OrdersViewModel(navigationStore));
            navDone = new NavigationCommand<OrdersViewModel>(navigationStore, () => new OrdersViewModel(navigationStore));

        }
    }
}
