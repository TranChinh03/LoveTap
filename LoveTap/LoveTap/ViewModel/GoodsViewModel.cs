using LoveTap.Commands;
using LoveTap.Model;
using LoveTap.Stores;
using LoveTap.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace LoveTap.ViewModel
{
    public class GoodsViewModel : BaseViewModel
    {
        public ICommand navDetail { get; set; }
        public ICommand navAddGood { get; set; }

        private ObservableCollection<SANPHAM> _ProductList;
        public ObservableCollection<SANPHAM> ProductList { get => _ProductList; set { _ProductList = value; OnPropertyChanged(); } }

        public GoodsViewModel(NavigationStore navigationStore)
        {
            ProductList = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs);
            navAddGood = new NavigationCommand<AddGoodViewModel>(navigationStore, () => new AddGoodViewModel(navigationStore));
            navDetail = new NavigationCommand<GoodDetailViewModel>(navigationStore, () => new GoodDetailViewModel(navigationStore));
        }

    }
}
