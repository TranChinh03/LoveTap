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
    internal class AddGoodViewModel:BaseViewModel
    {
        public ICommand navDone { get; set; }
        public ICommand navBack { get; set; }
        public AddGoodViewModel(NavigationStore navigationStore)
        {
            navDone = new NavigationCommand<GoodsViewModel>(navigationStore, () => new GoodsViewModel(navigationStore));
            navBack = new NavigationCommand<GoodsViewModel>(navigationStore, () => new GoodsViewModel(navigationStore));

        }

    }
}
