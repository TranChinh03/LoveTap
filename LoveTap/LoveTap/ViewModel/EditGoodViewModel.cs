using LoveTap.UserControlCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using LoveTap.Commands;
using LoveTap.Stores;

namespace LoveTap.ViewModel
{
    internal class EditGoodViewModel:BaseViewModel
    {
        public ICommand navDone { get; set; }
        public ICommand navBack { get; set; }
        public EditGoodViewModel(NavigationStore navigationStore)
        {
            navDone = new NavigationCommand<GoodDetailViewModel>(navigationStore, () => new GoodDetailViewModel(navigationStore));
            navBack = new NavigationCommand<GoodDetailViewModel>(navigationStore, () => new GoodDetailViewModel(navigationStore));
        }

    }
}
