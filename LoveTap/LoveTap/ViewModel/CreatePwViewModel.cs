using LoveTap.Commands;
using LoveTap.Stores;
using LoveTap.UserControlCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LoveTap.ViewModel
{
    internal class CreatePwViewModel:BaseViewModel
    {
        public ICommand navDone { get; set; }
        public ICommand navCancel { get; set; }

        public CreatePwViewModel(NavigationStore navigationStore)
        {
            navCancel = new NavigationCommand<ProfileUsrViewModel>(navigationStore, () => new ProfileUsrViewModel(navigationStore));
            navDone = new NavigationCommand<ProfileUsrViewModel>(navigationStore, () => new ProfileUsrViewModel(navigationStore));
        }
    }
}
