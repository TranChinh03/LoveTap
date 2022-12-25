using LoveTap.Commands;
using LoveTap.Model;
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
        public static HOADON OrderSelected { get; set; }


        public ICommand navAddOrder { get; set; }

        public ICommand navDetail { get; set; }
        public ICommand Detail { get; set; }

        private ObservableCollection<HOADON> _OrdersList;
        public ObservableCollection<HOADON> OrdersList { get => _OrdersList; set { _OrdersList = value; } }
        public OrdersViewModel(NavigationStore navigationStore)
        {
            OrdersList = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            navAddOrder = new NavigationCommand<AddOrdersViewModel>(navigationStore, () => new AddOrdersViewModel(navigationStore));
            navDetail = new NavigationCommand<OrderDetailViewModel>(navigationStore, () => new OrderDetailViewModel(navigationStore));
            Detail = new RelayCommand<OrderViewUC>((p) => { return p.OrderList.SelectedItem == null ? false : true; }, (p) => _DetailCs(p));
        }
        void _DetailCs(OrderViewUC p)
        {
            OrderSelected = (HOADON)p.OrderList.SelectedItem;
        }
    }
}
