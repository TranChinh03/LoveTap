using LoveTap.Commands;
using LoveTap.Model;
using LoveTap.Stores;
using LoveTap.UserControlCustom;
using LoveTap.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Xml.Linq;

namespace LoveTap.ViewModel
{
    public class CustomerViewModel:BaseViewModel
    {
        public ICommand navAddCustomer { get; set; }

        public ICommand navDetail { get; set; }
        public ICommand Detail { get; set; }

        private ObservableCollection<KHACHHANG> _CustomerList;
        public ObservableCollection<KHACHHANG> CustomerList { get => _CustomerList; set { _CustomerList = value; } }
        public static KHACHHANG CurrentSelected { get; set; }
        public CustomerViewModel(NavigationStore navigationStore)
        {
            CustomerList = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs.Where(x => x.DELETED == false));
            navAddCustomer = new NavigationCommand<AddCustomerViewModel>(navigationStore, () => new AddCustomerViewModel(navigationStore));
            navDetail = new NavigationCommand<CustomerDetailViewModel>(navigationStore, () => new CustomerDetailViewModel(navigationStore));
            Detail = new RelayCommand<CustomerViewUC>((p) => { return p.CustomerList.SelectedItem == null ? false : true; }, (p) => _DetailCs(p));
        }
        void _DetailCs(CustomerViewUC p)
        {
            CurrentSelected =(KHACHHANG)p.CustomerList.SelectedItem;
        }

    }
}
