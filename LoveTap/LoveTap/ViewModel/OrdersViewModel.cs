using LoveTap.Commands;
using LoveTap.Model;
using LoveTap.Service;
using LoveTap.Stores;
using LoveTap.UserControlCustom;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Xml.Linq;
using static LoveTap.ViewModel.CustomerViewModel;

namespace LoveTap.ViewModel
{
    public class OrdersViewModel : BaseViewModel
    {
        public static Order OrderSelected { get; set; }


        public ICommand navAddOrder { get; set; }

        public ICommand navDetail { get; set; }
        public ICommand Detail { get; set; }

        private ObservableCollection<HOADON> _OrdersList;
        public ObservableCollection<HOADON> OrdersList { get => _OrdersList; set { _OrdersList = value; } }


        private ObservableCollection<KHACHHANG> _CustomerList;
        public ObservableCollection<KHACHHANG> CustomerList { get => _CustomerList; set { _CustomerList = value; } }

        public struct Order
        {
            public int ID { get; set; }

            public int MAKH { get; set; }
            public string Phone { get; set; }
            public DateTime Date { get; set; }
            public double Total { get; set; }

            public int MANV { get; set; }

            public int MACN { get; set; }
        }

        private List<Order> _MyOrderList = new List<Order>();
        public List<Order> MyOrderList { get => _MyOrderList; set { _MyOrderList = value; OnPropertyChanged(); } }

        private List<Order> _OrderDateList = new List<Order>();
        public List<Order> OrderDateList { get => _OrderDateList; set { _OrderDateList = value; OnPropertyChanged(); } }


        private List<Order> _OrderSaleList = new List<Order>();
        public List<Order> OrderSaleList { get => _OrderSaleList; set { _OrderSaleList = value; OnPropertyChanged(); } }

        public IEnumerable<Order> MyFilterList
        {
            get
            {
                if (searchText == null && sortText == null)
                    return MyOrderList;
                else if (sortText != null && findText == "Customer ID")
                {
                    if (sortText == "Date")
                        return OrderDateList.Where(x => (x.MAKH.ToString().Contains(searchText.ToUpper())));
                    //else if (sortText == "Registation Date")
                    //    return CustomerRDList.Where(x => (x.HOTEN.ToUpper().Contains(searchText.ToUpper())));
                    else if (sortText == "Total")
                        return OrderSaleList.Where(x => (x.MAKH.ToString().Contains(searchText.ToUpper())));
                }
                else if (sortText != null && findText == "Phone")
                {
                    if (sortText == "Date")
                        return OrderDateList.Where(x => (x.Phone.ToString().Contains(searchText.ToUpper())));
                    //else if (sortText == "Registation Date")
                    //    return CustomerRDList.Where(x => (x.HOTEN.ToUpper().Contains(searchText.ToUpper())));
                    else if (sortText == "Total")
                        return OrderSaleList.Where(x => (x.Phone.ToString().Contains(searchText.ToUpper())));
                }
                //else if (sortText != null && findText == "Address")
                //{
                //    if (sortText == "Date of Birth")
                //        return CustomerDOBList.Where(x => (x.DIACHI.ToUpper().Contains(searchText.ToUpper())));
                //    else if (sortText == "Registation Date")
                //        return CustomerRDList.Where(x => (x.DIACHI.ToUpper().Contains(searchText.ToUpper())));
                //    else if (sortText == "Sale")
                //        return CustomerSaleList.Where(x => (x.DIACHI.ToUpper().Contains(searchText.ToUpper())));
                //}
                else if (sortText != null && findText == null)
                {
                    if (sortText == "Date")
                        return OrderDateList;
                    //else if (sortText == "Registation Date")
                    //    return CustomerRDList;
                    else if (sortText == "Total")
                        return OrderSaleList;
                }
                else if (sortText == null && findText != null)
                {
                    if (findText == "Customer ID")
                        return MyOrderList.Where(x => (x.MAKH.ToString().Contains(searchText.ToUpper())));
                    else if (findText == "Phone")
                        return MyOrderList.Where(x => (x.Phone.ToUpper().Contains(searchText.ToUpper())));
                    //else if (findText == "Address")
                    //    return MyCustomerList.Where(x => (x.DIACHI.ToUpper().Contains(searchText.ToUpper())));
                }
                return MyOrderList;
                //return ProductList.Where(x => ((x.TEN.ToUpper().Contains(searchText.ToUpper()))|| (x.MASP.ToUpper().Contains(searchText.ToUpper()))|| (x.GIA >= double.Parse(searchText))|| (x.MADM.ToUpper().Contains(searchText.ToUpper()))|| (x.CHITIET.ToUpper().Contains(searchText.ToUpper()))));

            }
        }

        void SwapInt(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        void SwapDouble(ref double a, ref double b)
        {
            double temp = a;
            a = b;
            b = temp;
        }

        void SwapString(ref string a, ref string b)
        {
            string temp = a;
            a = b;
            b = temp;
        }

        void SwapDateTime(ref DateTime a, ref DateTime b)
        {
            DateTime temp = a;
            a = b;
            b = temp;
        }
        void Swap(ref Order a, ref Order b)
        {
            int makha = a.MAKH;
            int makhb = b.MAKH;
            int mahda = a.ID;
            int mahdb = b.ID;
            string sdta = a.Phone;
            string sdtb = b.Phone;
            DateTime datea = (DateTime)a.Date;
            DateTime dateb = (DateTime)b.Date;
            double salea = (double)a.Total;
            double saleb = (double)b.Total;
            SwapInt(ref makha, ref makhb);
            SwapInt(ref mahda, ref mahdb);
            SwapString(ref sdta, ref sdtb);
            SwapDateTime(ref datea, ref dateb);
            SwapDouble(ref salea, ref saleb);
            a.MAKH = makha;
            a.ID = mahda;
            a.Phone = sdta;
            a.Date = datea;
            a.Total = salea;
            b.ID = mahdb;
            b.MAKH = makhb;
            b.Phone = sdtb;
            b.Date = datea;
            b.Total = saleb;
        }

        public List<Order> SapGiamDate(List<Order> list)
        {
            Order a, b;
            List<Order> list1 = new List<Order>();
            foreach (Order hd in list)
            {
                list1.Add(hd);
            }

            for (int i = 0; i < list1.Count() - 1; i++)
                for (int j = i + 1; j < list1.Count(); j++)
                {
                    if (list1[i].Date < list[j].Date)
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

        //public List<KHACHHANG> SapGiamRD(List<KHACHHANG> list)
        //{
        //    KHACHHANG a, b;
        //    List<KHACHHANG> list1 = new List<KHACHHANG>();
        //    foreach (KHACHHANG kh in list)
        //    {
        //        list1.Add(kh);
        //    }

        //    for (int i = 0; i < list1.Count() - 1; i++)
        //        for (int j = i + 1; j < list1.Count(); j++)
        //        {
        //            if (list1[i].NGDK < list[j].NGDK)
        //            {
        //                a = list1[i];
        //                b = list1[j];
        //                Swap(ref a, ref b);
        //                list1[i] = a;
        //                list1[j] = b;
        //            }

        //        }
        //    return list1;
        //}

        public List<Order> SapGiamSale(List<Order> list)
        {
            Order a, b;
            List<Order> list1 = new List<Order>();
            foreach (Order hd in list)
            {
                list1.Add(hd);
            }

            for (int i = 0; i < list1.Count() - 1; i++)
                for (int j = i + 1; j < list1.Count(); j++)
                {
                    if (list1[i].Total < list[j].Total)
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

        private string _findText;
        public string findText
        {
            get { return _findText; }
            set
            {
                _findText = value;
                OnPropertyChanged("findText");
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

        private string _branchText;
        public string branchText
        {
            get { return _branchText; }
            set
            {
                _branchText = value;
                OnPropertyChanged("branchText");
                OnPropertyChanged("MyFilterList");
            }
        }


        public int[] BranchIDList { get; set; } = new int[DataProvider.Ins.DB.CHINHANHs.Count()];





        public OrdersViewModel(NavigationStore navigationStore)
        {
            
            OrdersList = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs.Where(x => x.DELETED == false));
            CustomerList = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs.Where(x => x.DELETED == false));
            navAddOrder = new NavigationCommand<AddOrdersViewModel>(navigationStore, () => new AddOrdersViewModel(navigationStore));
            navDetail = new NavigationCommand<OrderDetailViewModel>(navigationStore, () => new OrderDetailViewModel(navigationStore));
            //navDetail = new ParameterNavigationService<string, OrderDetailViewModel>(navigationStore, (TextTest) => new OrderDetailViewModel(( TextTest), navigationStore));
            //ParameterNavigationService<string, OrderDetailViewModel> navigationService = new ParameterNavigationService<string, OrderDetailViewModel>(navigationStore,
            //    (parameter) => new OrderDetailViewModel(parameter, navigationStore));
            //navDetail = new ChangeOrdCommand(this,TextTest, navigationService);
            Detail = new RelayCommand<OrderViewUC>((p) => { return p.OrderList.SelectedItem == null ? false : true; }, (p) => _DetailCs(p));

            Order hoadon = new Order();

            foreach (HOADON hd in OrdersList.Where(x=> x.DELETED == false))
            {
                hoadon.ID = hd.MAHD ;
                hoadon.MAKH = (int)hd.MAKH ;
                hoadon.Date = (DateTime)hd.NGMUA;
                hoadon.Total =(double) hd.TONGTIEN;
                hoadon.MANV = (int)hd.MANV;
                foreach (KHACHHANG kh in CustomerList)
                {
                    if(kh.MAKH == hoadon.MAKH)
                    {
                        hoadon.Phone = kh.SDT;
                    }
                }
                MyOrderList.Add(hoadon);
            }

            OrderDateList = SapGiamDate(MyOrderList);
            OrderSaleList = SapGiamSale(MyOrderList);

        }
        void _DetailCs(OrderViewUC p)
        {
            OrderSelected = (Order)p.OrderList.SelectedItem;
        }
    }
}
