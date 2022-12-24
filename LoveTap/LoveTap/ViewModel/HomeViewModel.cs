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

namespace LoveTap.ViewModel
{
    public class HomeViewModel: BaseViewModel
    {

        private ObservableCollection<SANPHAM> _ProductList;
        public ObservableCollection<SANPHAM> ProductList { get => _ProductList; set { _ProductList = value; OnPropertyChanged(); } }

        private ObservableCollection<CTHD> _OrdersDetailList;
        public ObservableCollection<CTHD> OrdersDetailList { get => _OrdersDetailList; set { _OrdersDetailList = value; OnPropertyChanged(); } }

        private ObservableCollection<TONKHO> _StockList;
        public ObservableCollection<TONKHO> StockList { get => _StockList; set { _StockList = value; OnPropertyChanged(); } }

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
        public ICommand GetIdButton { get; set; }

        public ICommand SwitchTab { get; set; }
        public ICommand Detail { get; set; }

        public ICommand LoadHome { get; set; }
        string Name;

        public struct BestSelling
        {
            public string Ten { get; set; }
            public int SoLuong { get; set; }
            public int SoLuongTon { get; set; }
        }

        private List<BestSelling> _BestSellingList = new List<BestSelling>();
        public List<BestSelling> BestSellingList { get => _BestSellingList ; set { _BestSellingList = value; } }
        public HomeViewModel(NavigationStore navigationStore)
        {
            
            LoadHome = new RelayCommand<Page>((p) => true, (p) =>
            {
                ProductList = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs);
                OrdersDetailList = new ObservableCollection<CTHD>(DataProvider.Ins.DB.CTHDs);
                StockList = new ObservableCollection<TONKHO>(DataProvider.Ins.DB.TONKHOes);

                int[] sum= new int [DataProvider.Ins.DB.SANPHAMs.Count()];
                string[] name = new string [DataProvider.Ins.DB.SANPHAMs.Count()];
                int[] stock = new int[DataProvider.Ins.DB.SANPHAMs.Count()];


                for (int i = 0; i < DataProvider.Ins.DB.SANPHAMs.Count(); i++)
                {
                    int tong = 0;
                    for(int j = 0; j < DataProvider.Ins.DB.CTHDs.Count(); j++)
                    {
                        if (ProductList[i].MASP == OrdersDetailList[j].MASP)
                            tong += (int)OrdersDetailList[j].SOLUONG;
                    }
                    sum[i] = tong;
                    name[i] = ProductList[i].TEN.ToString();
                    for(int k = 0; k < DataProvider.Ins.DB.TONKHOes.Count(); k++)
                    {
                        if (ProductList[i].MASP.ToString()==StockList[k].MASP.ToString())
                            stock [i] = int.Parse(StockList[k].SOLUONG.ToString());
                    }
                }

                for(int i=0;i<sum.Length -1 ;i++)
                {
                    for(int j= i + 1;j<sum.Length;j++)
                    {
                        if (sum[j] > sum[i])
                        {
                            Swap1(ref sum[j],ref sum[i]);
                            Swap2(ref name[j],ref name[i]);
                            Swap1(ref stock[j], ref stock[i]);
                        }
                    }
                }

                for (int i= 0;i<sum.Length ;i++)
                {
                    BestSelling best = new BestSelling();
                    best.Ten = name[i];
                    best.SoLuong = sum[i];
                    best.SoLuongTon = stock[i];
                    BestSellingList.Add(best);
                }


            });

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

            GetIdButton = new RelayCommand<Button>((p) => true, (p) => Name = p.Uid);
            SwitchTab = new RelayCommand<HomeScreen>((p) => true, (p) => switchtab(p));
            Detail = new RelayCommand<HomeScreen>((p) => { return p.BestSellingList.SelectedItem == null ? false : true; }, (p) => _DetailCs(p));

        }

        

        void switchtab(HomeScreen p)
        {
            int index = int.Parse(Name);
            switch (index)
            {
                case 1:
                    {
                        //p.AddCustomer.Visibility=Visibility.Visible;
                        break;
                    }
            }
        }

        void _DetailCs(HomeScreen p)
        {
           
            p.DetailBestSl.Visibility= Visibility.Visible;
            
        }

    }
}
