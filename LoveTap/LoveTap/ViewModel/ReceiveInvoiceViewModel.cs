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
    public class ReceiveInvoiceViewModel : BaseViewModel
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

        private string _SupplierName;
        public string SupplierName { get => _SupplierName; set { _SupplierName = value; OnPropertyChanged(); } }


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

        private ObservableCollection<NHACUNGCAP> _SupplierList;
        public ObservableCollection<NHACUNGCAP> SupplierList { get => _SupplierList; set { _SupplierList = value; OnPropertyChanged(); } }

        private ObservableCollection<PHIEUNHAP> _ReceiveList;
        public ObservableCollection<PHIEUNHAP> ReceiveList { get => _ReceiveList; set { _ReceiveList = value; OnPropertyChanged(); } }

        private ObservableCollection<CTPN> _ReceiveDetailList;
        public ObservableCollection<CTPN> ReceiveDetailList { get => _ReceiveDetailList; set { _ReceiveDetailList = value; OnPropertyChanged(); } }

        private ObservableCollection<SANPHAM> _ProductList;
        public ObservableCollection<SANPHAM> ProductList { get => _ProductList; set { _ProductList = value; OnPropertyChanged(); } }

        private ObservableCollection<CHINHANH> _BranchList;
        public ObservableCollection<CHINHANH> BranchList { get => _BranchList; set { _BranchList = value; OnPropertyChanged(); } }

        public struct Receive
        {
            public int Stt { get; set; }
            public string Ten { get; set; }
            public double Gia { get; set; }
            public int SoLuong { get; set; }
            public double TongTien { get; set; }
        }

        private List<Receive> _MyReceiveList = new List<Receive>();
        public List<Receive> MyReceiveList { get => _MyReceiveList; set { _MyReceiveList = value; } }

        public ReceiveInvoiceViewModel(NavigationStore navigationStore)
        {
            navBack = new NavigationCommand<OrderDetailViewModel>(navigationStore, () => new OrderDetailViewModel(navigationStore));

            ProductList = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs);
            ReceiveList = new ObservableCollection<PHIEUNHAP>(DataProvider.Ins.DB.PHIEUNHAPs);
            ReceiveDetailList = new ObservableCollection<CTPN>(DataProvider.Ins.DB.CTPNs);
            SupplierList = new ObservableCollection<NHACUNGCAP>(DataProvider.Ins.DB.NHACUNGCAPs);
            EmployeeList = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs);
            BranchList = new ObservableCollection<CHINHANH>(DataProvider.Ins.DB.CHINHANHs);

            ReceiveOrderViewModel.Receive temp = ReceiveOrderViewModel.ReceiveSelected;

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

            foreach (NHACUNGCAP ncc in DataProvider.Ins.DB.NHACUNGCAPs.Where(x => x.MANCC == temp.MANCC))
            {
                SupplierName = ncc.TEN.ToString();
                Phone = ncc.SDT.ToString();
                Address = ncc.DIACHI.ToString();
            }
            int stt = 0;

            for (int i = 0; i < DataProvider.Ins.DB.CTPNs.Count(); i++)
            {
                if (ReceiveDetailList[i].MAPN == ID)
                {
                    Receive order = new Receive();
                    for (int j = 0; j < DataProvider.Ins.DB.SANPHAMs.Count(); j++)
                    {
                        if (ProductList[j].MASP == ReceiveDetailList[i].MASP)
                        {
                            stt++;
                            order.Stt = stt;
                            order.Ten = ProductList[j].TEN;
                            order.Gia = (double)ProductList[j].GIA;
                            order.SoLuong = (int)ReceiveDetailList[i].SOLUONG;
                            order.TongTien = order.SoLuong * order.Gia;
                            SubTotal += order.TongTien;
                            MyReceiveList.Add(order);
                        }

                    }

                }
            }
        }
    }
}
