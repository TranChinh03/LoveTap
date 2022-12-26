using LoveTap.Commands;
using LoveTap.Model;
using LoveTap.Stores;
using LoveTap.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace LoveTap.ViewModel
{
    public class GoodsViewModel : BaseViewModel
    {
        public ICommand navDetail { get; set; }
        public ICommand navAddGood { get; set; }

        private ObservableCollection<SANPHAM> _ProductList;
        public ObservableCollection<SANPHAM> ProductList { get => _ProductList; set { _ProductList = value; OnPropertyChanged(); } }

        private ObservableCollection<CTHD> _OrderDetailList;
        public ObservableCollection<CTHD> OrderDetailList { get => _OrderDetailList; set { _OrderDetailList = value; OnPropertyChanged(); } }

        private ObservableCollection<TONKHO> _StockList;
        public ObservableCollection<TONKHO> StockList { get => _StockList; set { _StockList = value; OnPropertyChanged(); } }

        public struct Product
        {
            public string Ten { get; set; }
            public int SoLuong { get; set; }
            public int SoLuongTon { get; set; }
        }

        private List<Product> _MyProductList = new List<Product>();
        public List<Product> MyProductList { get => _MyProductList; set { _MyProductList = value; } }


        private List<Product> _MyProductOrdersList = new List<Product>();
        public List<Product> MyProductOrdersList { get => _MyProductOrdersList; set { _MyProductOrdersList = value; } }


        private List<Product> _MyProductStocksList = new List<Product>();
        public List<Product> MyProductStocksList { get => _MyProductStocksList; set { _MyProductStocksList = value; } }



        //private List<SANPHAM> _MyFilteredList = new List<SANPHAM>();
        //public List<SANPHAM> MyFilterList { get => _MyFilteredList; set { _MyFilteredList = value; } }
        public IEnumerable<Product> MyFilterList
        {
            get
            {
                if (searchText == null && sortText == null)
                    return MyProductList;
                else if (searchText == null && sortText == "Orders")
                    return MyProductOrdersList;
                else if (searchText == null && sortText == "Stocks")
                    return MyProductStocksList;
                else if (searchText != null && sortText == "Orders")
                    return MyProductOrdersList.Where(x => (x.Ten.ToUpper().Contains(searchText.ToUpper())));
                else
                    return MyProductStocksList.Where(x => (x.Ten.ToUpper().Contains(searchText.ToUpper())));
                //return ProductList.Where(x => ((x.TEN.ToUpper().Contains(searchText.ToUpper()))|| (x.MASP.ToUpper().Contains(searchText.ToUpper()))|| (x.GIA >= double.Parse(searchText))|| (x.MADM.ToUpper().Contains(searchText.ToUpper()))|| (x.CHITIET.ToUpper().Contains(searchText.ToUpper()))));

            }
        }
        private string _searchText;
        public string searchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged("searchText");
                OnPropertyChanged("MyFilterList");
            }
        }

        private string _sortText;
        public string sortText
        {
            get { return _sortText; }
            set
            {
                _sortText = value;
                OnPropertyChanged("sortText");
                OnPropertyChanged("MyFilterList");
            }
        }

        void SwapString(ref string a, ref string b)
        {
            string temp = a;
            a = b;
            b = temp;
        }

        void SwapInt( ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        void Swap( ref Product a, ref Product b)
        {
            int sli, slj, slti, sltj;
            string teni, tenj;
            teni = a.Ten;
            tenj = b.Ten;
            sli = a.SoLuong;
            slj = b.SoLuong;
            slti = a.SoLuongTon;
            sltj = b.SoLuongTon;
            SwapInt(ref sli, ref slj);
            SwapInt(ref slti, ref sltj);
            SwapString(ref teni, ref tenj);
            a.Ten = teni;
            a.SoLuong = sli;
            a.SoLuongTon = slti;
            b.Ten = tenj;
            b.SoLuong = slj;
            b.SoLuongTon = sltj;
        }
        public List<Product> SapGiamOrders(List<Product> list)
        {
            Product a, b;
            List<Product> list1 = new List<Product>();
            foreach (Product product in list)
            {
                list1.Add(product);
            }
            for (int i=0;i<list1.Count()-1;i++)
                for(int j = i+1; j< list1.Count();j++)
                {
                    if (list1[i].SoLuong < list1[j].SoLuong)
                    {
                        a = list1[i];
                        b = list1[j];
                        Swap( ref a, ref b);
                        list1[i] = a;
                        list1[j] = b;
                    }
                }
            return list1;
        }

        public List<Product> SapGiamStock(List<Product> list)
        {
            Product a, b;
            List<Product> list1 = new List<Product>();
            foreach (Product product in list)
            {
                list1.Add(product);
            }
            for (int i = 0; i < list1.Count() - 1; i++)
                for (int j = i + 1; j < list1.Count(); j++)
                {
                    if (list1[i].SoLuongTon < list1[j].SoLuongTon)
                    {
                        a = list1[i];
                        b = list1[j];
                        Swap(ref a, ref b);
                        list1[i] = a;
                        list1[j] = b;
                    }
                }
            return list1;
        }


        public GoodsViewModel(NavigationStore navigationStore)

        {

            ProductList = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs);
            OrderDetailList = new ObservableCollection<CTHD>(DataProvider.Ins.DB.CTHDs);
            StockList = new ObservableCollection<TONKHO>(DataProvider.Ins.DB.TONKHOes);

            foreach (SANPHAM sp in ProductList)
            {
                Product product = new Product();
                int soluong = 0;
                int soluongton = 0;
                foreach (CTHD cthd in OrderDetailList.Where(x => x.MASP == sp.MASP))
                {
                    soluong += (int)cthd.SOLUONG;
                }

                foreach(TONKHO tk in StockList.Where(x => x.MASP == sp.MASP))
                {
                    soluongton += (int)tk.SOLUONG;
                }

                product.SoLuongTon = soluongton;
                product.SoLuong = soluong;
                product.Ten = sp.TEN;
                MyProductList.Add(product);
            }
            MyProductOrdersList = SapGiamOrders(MyProductList);
            MyProductStocksList = SapGiamStock(MyProductList);
            

            navAddGood = new NavigationCommand<AddGoodViewModel>(navigationStore, () => new AddGoodViewModel(navigationStore));
            navDetail = new NavigationCommand<GoodDetailViewModel>(navigationStore, () => new GoodDetailViewModel(navigationStore));
        }

    }
}
