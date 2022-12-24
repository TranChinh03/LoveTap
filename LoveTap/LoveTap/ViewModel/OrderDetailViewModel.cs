using LoveTap.UserControlCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using LoveTap.Stores;
using LoveTap.Commands;

namespace LoveTap.ViewModel
{
    internal class OrderDetailViewModel:BaseViewModel
    {
        public ICommand navBack { get; set; }
        public OrderDetailViewModel(NavigationStore navigationStore)
        {
            navBack = new NavigationCommand<OrdersViewModel>(navigationStore, () => new OrdersViewModel(navigationStore));
        }

    }
}
