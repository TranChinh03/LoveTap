using LoveTap.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using LoveTap.Stores;
using LoveTap.UserControlCustom;

namespace LoveTap.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {

        private ObservableCollection<SANPHAM> _ProductList;
        public ObservableCollection<SANPHAM> ProductList { get => _ProductList; set { _ProductList = value; OnPropertyChanged(); } }

        private ObservableCollection<CTHD> _OrdersDetailList;
        public ObservableCollection<CTHD> OrdersDetailList { get => _OrdersDetailList; set { _OrdersDetailList = value; OnPropertyChanged(); } }

        private ObservableCollection<TONKHO> _StockList;
        public ObservableCollection<TONKHO> StockList { get => _StockList; set { _StockList = value; OnPropertyChanged(); } }

        private ObservableCollection<HOADON> _OrderList;
        public ObservableCollection<HOADON> OrderList { get => _OrderList; set { _OrderList = value; OnPropertyChanged(); } }

        private ObservableCollection<KHACHHANG> _CustomerList;
        public ObservableCollection<KHACHHANG> CustomerList { get => _CustomerList; set { _CustomerList = value; OnPropertyChanged(); } }


        private string _ProductID1;
        public string ProductID1 { get => _ProductID1; set { _ProductID1 = value; OnPropertyChanged(); } }

        private string _ProductID2;
        public string ProductID12 { get => _ProductID2; set { _ProductID2 = value; OnPropertyChanged(); } }

        private string _ProductName;
        public string ProductName { get => _ProductName; set { _ProductName = value; OnPropertyChanged(); } }

        private string _Quantity;
        public string Quantity { get => _Quantity; set { _Quantity = value; OnPropertyChanged(); } }

        private string _Stock;
        public string Stock { get => _Stock; set { _Stock = value; OnPropertyChanged(); } }
        public ICommand ChangeDate { get; set; }

        public ICommand SwitchTab { get; set; }
        public ICommand Detail { get; set; }

        public ICommand LoadHome { get; set; }
        string Name;
        public Nullable<double> _Doanhthu { get; set; }
        public Nullable<double> Doanhthu { get => _Doanhthu; set { _Doanhthu = value; OnPropertyChanged(); } }
        public DateTime _selectedDate { get; set; }
        public DateTime selectedDate { get => _selectedDate; set {_selectedDate = value; OnPropertyChanged(); } }
        public int _slOrders { get; set; }
        public int slOrders { get => _slOrders; set { _slOrders = value; OnPropertyChanged(); } }
        public int _slKhachHang { get; set; }
        public int slKhachHang { get => _slKhachHang; set { _slKhachHang = value; OnPropertyChanged(); } }
        public int SoLuongTon { get; set; }
        public struct BestSelling
        {
            public string Ten { get; set; }
            public int SoLuong { get; set; }
            public int SoLuongTon { get; set; }
        }

        private List<BestSelling> _BestSellingList = new List<BestSelling>();
        public List<BestSelling> BestSellingList { get => _BestSellingList; set { _BestSellingList = value; } }
        public HomeViewModel(NavigationStore navigationStore)
        {
            //Load Home
            LoadHome = new RelayCommand<HomeViewUC>((p) => true, (p) =>
            {
                ProductList = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs);
                OrderList = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
                CustomerList = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
                OrdersDetailList = new ObservableCollection<CTHD>(DataProvider.Ins.DB.CTHDs);
                StockList = new ObservableCollection<TONKHO>(DataProvider.Ins.DB.TONKHOes);

                int[] sum = new int[DataProvider.Ins.DB.SANPHAMs.Count()];
                string[] name = new string[DataProvider.Ins.DB.SANPHAMs.Count()];
                int[] stock = new int[DataProvider.Ins.DB.SANPHAMs.Count()];


                for (int i = 0; i < DataProvider.Ins.DB.SANPHAMs.Count(); i++)
                {
                    int tong = 0;
                    for (int j = 0; j < DataProvider.Ins.DB.CTHDs.Count(); j++)
                    {
                        if (ProductList[i].MASP == OrdersDetailList[j].MASP)
                            tong += (int)OrdersDetailList[j].SOLUONG;
                    }
                    sum[i] = tong;
                    name[i] = ProductList[i].TEN.ToString();
                    for (int k = 0; k < DataProvider.Ins.DB.TONKHOes.Count(); k++)
                    {
                        if (ProductList[i].MASP.ToString()==StockList[k].MASP.ToString())
                            stock[i] = int.Parse(StockList[k].SOLUONG.ToString());
                    }
                }

                for (int i = 0; i<sum.Length -1; i++)
                {
                    for (int j = i + 1; j<sum.Length; j++)
                    {
                        if (sum[j] > sum[i])
                        {
                            Swap1(ref sum[j], ref sum[i]);
                            Swap2(ref name[j], ref name[i]);
                            Swap1(ref stock[j], ref stock[i]);
                        }
                    }
                }

                for (int i = 0; i<sum.Length; i++)
                {
                    BestSelling best = new BestSelling();
                    best.Ten = name[i];
                    best.SoLuong = sum[i];
                    best.SoLuongTon = stock[i];
                    BestSellingList.Add(best);
                }
                Statistic();
            });

            //SelectedDay
            void _SelectedDate(Calendar p)
            {
                selectedDate= p.SelectedDate.Value;
                Statistic();
            }

            void Swap1(ref int a, ref int b)
            {
                int temp = a;
                a = b;
                b = temp;
            }

            void Swap2(ref string a, ref string b)
            {
                string temp = a;
                a = b;
                b = temp;
            }

            ChangeDate = new RelayCommand<Calendar>((p) => { return p.SelectedDates == null ? false : true; }, (p) => _SelectedDate(p));
            //Detail = new RelayCommand<HomeViewUC>((p) => { return p.cale.SelectedItem == null ? false : true; }, (p) => _DetailCs(p));
            selectedDate= DateTime.Now.Date;

        }

        void Statistic()
        {
            slKhachHang = 0; slOrders= 0; Doanhthu = 0;
            for (int i = 0; i < DataProvider.Ins.DB.HOADONs.Count(); i++)
                if (OrderList[i].NGMUA==selectedDate)
                {
                    slOrders++;
                    Doanhthu += OrderList[i].TONGTIEN;
                };
            for (int i = 0; i < DataProvider.Ins.DB.KHACHHANGs.Count(); i++)
                if (CustomerList[i].NGDK==selectedDate)
                    slKhachHang++;
        }

        

        void _DetailCs(HomeScreen p)
        {

            p.DetailBestSl.Visibility= Visibility.Visible;

        }

    }
}
