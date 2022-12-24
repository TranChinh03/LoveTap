using LoveTap.Commands;
using LoveTap.Stores;
using LoveTap.UserControlCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace LoveTap.ViewModel
{
    internal class AddCustomerViewModel:BaseViewModel
    {
        public ICommand navDone { get; set; }
        public ICommand navBack { get; set; }
        public AddCustomerViewModel(NavigationStore navigationStore) {
            navDone = new NavigationCommand<CustomerViewModel>(navigationStore, () => new CustomerViewModel(navigationStore));
            navBack = new NavigationCommand<CustomerViewModel>(navigationStore, () => new CustomerViewModel(navigationStore));

        }

    }
}
