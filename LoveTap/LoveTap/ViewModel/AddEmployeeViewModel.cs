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
    internal class AddEmployeeViewModel:BaseViewModel
    {
        public ICommand navDone { get; set; }
        public ICommand navBack { get; set; }

        public AddEmployeeViewModel(NavigationStore navigationStore)
        {
            navDone = new NavigationCommand<EmployeeViewModel>(navigationStore, () => new EmployeeViewModel(navigationStore));
            navBack = new NavigationCommand<EmployeeViewModel>(navigationStore, () => new EmployeeViewModel(navigationStore));
        }
    }
}
