using LoveTap.Model;
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
using System.Windows.Navigation;
using LoveTap.Stores;
using LoveTap.Commands;

namespace LoveTap.ViewModel
{
    public class AddOrdersViewModel : BaseViewModel
    {
        public ICommand navBack { get; set; }
        public ICommand navDone { get; set; }
        

        private ObservableCollection<HOADON> _OrdersList;
        public ObservableCollection<HOADON> OrdersList { get => _OrdersList; set { _OrdersList = value; } }
        public AddOrdersViewModel(NavigationStore navigationStore)
        {
            navBack = new NavigationCommand<OrdersViewModel>(navigationStore, () => new OrdersViewModel(navigationStore));
            navDone = new NavigationCommand<OrdersViewModel>(navigationStore, () => new OrdersViewModel(navigationStore));

        }
    }
}
