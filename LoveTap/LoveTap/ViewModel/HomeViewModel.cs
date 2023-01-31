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
using LoveTap.Commands;

namespace LoveTap.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        public static BestSelling BestslSelected { get; set; }
        public static int flag { get; set; }

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
        public ICommand navStatistic { get; set; }
        public ICommand updateTabName { get; set; }
        public ICommand updateCbb { get; set; }
        public ICommand updateCbb2 { get; set; }
        public ICommand updateCbb3 { get; set; }

        public ICommand navDetail { get; set; }
        public ICommand Detail { get; set; }

        public ICommand LoadHome { get; set; }
        string Name;
        public Nullable<double> _SanPham { get; set; }
        public Nullable<double> SanPham { get => _SanPham; set { _SanPham = value; OnPropertyChanged(); } }
        public DateTime _selectedDate { get; set; }
        public DateTime selectedDate { get => _selectedDate; set { _selectedDate = value; OnPropertyChanged(); } }
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

        void _UpdateCbb(StatisticViewUC p)
        {
            StatisticViewModel.cbbTypeSlted=0;
        }
        void _UpdateCbb2(StatisticViewUC p)
        {
            StatisticViewModel.cbbTypeSlted=1;
        }
        void _UpdateCbb3(StatisticViewUC p)
        {
            StatisticViewModel.cbbTypeSlted=2;
        }
        public HomeViewModel(NavigationStore navigationStore)
        {

            ProductList = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs);
            OrderList = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            CustomerList = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
            OrdersDetailList = new ObservableCollection<CTHD>(DataProvider.Ins.DB.CTHDs);
            StockList = new ObservableCollection<TONKHO>(DataProvider.Ins.DB.TONKHOes);

            ChangeDate = new RelayCommand<Calendar>((p) => { return p.SelectedDates == null ? false : true; }, (p) => _SelectedDate(p));
            selectedDate= DateTime.Now.Date;
            Detail = new RelayCommand<HomeViewUC>((p) => { return p.BestSellingList.SelectedItem == null ? false : true; }, (p) => _DetailCs(p));
            navDetail = new NavigationCommand<GoodDetailViewModel>(navigationStore, () => new GoodDetailViewModel(navigationStore));
            navStatistic = new NavigationCommand<StatisticViewModel>(navigationStore, () => new StatisticViewModel(navigationStore));
            
            updateTabName = new RelayCommand<MainWd>((p) => { return true; }, (p) => _UpdateTabName(p));
            updateCbb = new RelayCommand<StatisticViewUC>((p) => { return true; }, (p) => _UpdateCbb(p));
            updateCbb2 = new RelayCommand<StatisticViewUC>((p) => { return true; }, (p) => _UpdateCbb2(p));
            updateCbb3 = new RelayCommand<StatisticViewUC>((p) => { return true; }, (p) => _UpdateCbb3(p));

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
        }

        void _UpdateTabName(MainWd  p)
        {
            p.Tabtxbl.Text="Statistic";
        }
        
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

        void Statistic()
        {
            slKhachHang = 0; slOrders= 0; SanPham = 0;
            for (int i = 0; i < DataProvider.Ins.DB.HOADONs.Count(); i++)
                if (OrderList[i].NGMUA==selectedDate)
                {
                    slOrders++;
                    for (int j = 0; j < DataProvider.Ins.DB.CTHDs.Count(); j++)
                        if (OrdersDetailList[j].MAHD == OrderList[i].MAHD)
                            SanPham += OrdersDetailList[j].SOLUONG;
                };
            for (int i = 0; i < DataProvider.Ins.DB.KHACHHANGs.Count(); i++)
                if (CustomerList[i].NGDK==selectedDate)
                    slKhachHang++;
        }



        void _DetailCs(HomeViewUC p)
        {
            BestslSelected = (BestSelling)p.BestSellingList.SelectedItem;
            flag = 1;
        }

    }
}
