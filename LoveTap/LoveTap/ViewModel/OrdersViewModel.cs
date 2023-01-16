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
        }

        private List<Order> _MyOrdersList = new List<Order>();
        public List<Order> MyOrdersList { get => _MyOrdersList; set { _MyOrdersList = value; } }

        public OrdersViewModel(NavigationStore navigationStore)
        {
            
            OrdersList = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            CustomerList = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
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
                MyOrdersList.Add(hoadon);
            }

        }
        void _DetailCs(OrderViewUC p)
        {
            OrderSelected = (Order)p.OrderList.SelectedItem;
        }
    }
}
