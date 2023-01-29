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
    public class ReceiveDetailViewModel : BaseViewModel
    {
        public ICommand navBack { get; set; }
        public ICommand navDone { get; set; }

        public ICommand navPrint { get; set; }

        public ICommand DeleteCommand { get; set; }

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


        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }


        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }


        private string _EmployeeName;
        public string EmployeeName { get => _EmployeeName; set { _EmployeeName = value; OnPropertyChanged(); } }

        private int _EmployeeID;
        public int EmployeeID { get => _EmployeeID; set { _EmployeeID = value; OnPropertyChanged(); } }

        private string _Branch;
        public string Branch { get => _Branch; set { _Branch = value; OnPropertyChanged(); } }

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

        public struct Receive
        {
            public string Ten { get; set; }
            public double Gia { get; set; }
            public int SoLuong { get; set; }
            public double TongTien { get; set; }
        }

        private List<Receive> _MyRecieveList = new List<Receive>();
        public List<Receive> MyReceiveList { get => _MyRecieveList; set { _MyRecieveList = value; } }
        public ICommand LoadedReceiveDetail { get; set; }
        string Name;
        public ReceiveDetailViewModel(NavigationStore navigationStore)
        {
            navBack = new NavigationCommand<ReceiveOrderViewModel>(navigationStore, () => new ReceiveOrderViewModel(navigationStore));

            navDone = new NavigationCommand<ReceiveOrderViewModel>(navigationStore, () => new ReceiveOrderViewModel(navigationStore));


            navPrint = new NavigationCommand<ReceiveInvoiceViewModel>(navigationStore, () => new ReceiveInvoiceViewModel(navigationStore));

            LoadedReceiveDetail = new RelayCommand<UserControl>((p) => true, (p) =>
            {
                ProductList = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs);
                ReceiveList = new ObservableCollection<PHIEUNHAP>(DataProvider.Ins.DB.PHIEUNHAPs);
                ReceiveDetailList = new ObservableCollection<CTPN>(DataProvider.Ins.DB.CTPNs);
                EmployeeList = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs);
                SupplierList = new ObservableCollection<NHACUNGCAP>(DataProvider.Ins.DB.NHACUNGCAPs);

                ReceiveOrderViewModel.Receive temp = ReceiveOrderViewModel.ReceiveSelected;

                ID = (int)temp.ID;
                Date = (DateTime)temp.Date;
                SubTotal = 0;



                for (int i = 0; i < DataProvider.Ins.DB.CTPNs.Count(); i++)
                {
                    if (ReceiveDetailList[i].MAPN == ID)
                    {
                        Receive receive = new Receive();
                        for (int j = 0; j < DataProvider.Ins.DB.SANPHAMs.Count(); j++)
                        {
                            if (ProductList[j].MASP == ReceiveDetailList[i].MASP)
                            {
                                receive.Ten = ProductList[j].TEN;
                                receive.Gia = (double)ProductList[j].GIA;
                                receive.SoLuong = (int)ReceiveDetailList[i].SOLUONG;
                                receive.TongTien = receive.SoLuong * receive.Gia;
                                SubTotal += receive.TongTien;
                                MyReceiveList.Add(receive);
                            }

                        }

                    }
                }

                foreach (NHANVIEN nv in DataProvider.Ins.DB.NHANVIENs.Where(x => x.MANV == temp.MANV))
                {
                    EmployeeName = nv.HOTEN.ToString();
                    EmployeeID = nv.MANV;
                    Branch = nv.MACN.ToString();
                }

                foreach (NHACUNGCAP ncc in DataProvider.Ins.DB.NHACUNGCAPs.Where(x => x.MANCC == temp.MANCC))
                {
                    SupplierName = ncc.TEN.ToString();
                    Email = ncc.EMAIL.ToString();
                    Address = ncc.DIACHI.ToString();
                }

            });

            DeleteCommand = new RelayCommand<UserControl>((p) => true, (p) =>
            {
                var receive = DataProvider.Ins.DB.PHIEUNHAPs.Where(x => x.MAPN == ID).SingleOrDefault();
                receive.DELETED = true;
                DataProvider.Ins.DB.SaveChanges();

            });
        }
    }
}
