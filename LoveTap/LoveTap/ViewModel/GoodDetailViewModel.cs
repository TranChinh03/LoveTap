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
    internal class GoodDetailViewModel:BaseViewModel
    {
        public ICommand navEdit { get; set; }
        public ICommand navBack { get; set; }
        public GoodDetailViewModel(NavigationStore navigationStore)
        {
            navEdit = new NavigationCommand<EditGoodViewModel>(navigationStore, () => new EditGoodViewModel(navigationStore));
            navBack = new NavigationCommand<GoodsViewModel>(navigationStore, () => new GoodsViewModel(navigationStore));
        }

    }
}
