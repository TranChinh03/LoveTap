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
using LoveTap.Model;
using System.Collections.ObjectModel;

namespace LoveTap.ViewModel
{
    internal class GoodDetailViewModel:BaseViewModel
    {
        public ICommand navEdit { get; set; }
        public ICommand navBack { get; set; }

        private string _ID { get; set; }
        public string ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }

        private string _Name { get; set; }
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }

        private string _ProductID { get; set; }
        public string ProductID { get => _ProductID; set { _ProductID = value; OnPropertyChanged(); } }

        private string _Brand { get; set; }
        public string Brand { get => _Brand; set { _Brand = value; OnPropertyChanged(); } }

        private double _Price { get; set; }
        public double Price { get => _Price; set { _Price = value; OnPropertyChanged(); } }

        private string _RAM { get; set; }
        public string RAM { get => _RAM; set { _RAM = value; OnPropertyChanged(); } }

        private string _CPU { get; set; }
        public string CPU { get => _CPU; set { _CPU = value; OnPropertyChanged(); } }

        private string _HD { get; set; }
        public string HD { get => _HD; set { _HD = value; OnPropertyChanged(); } }
        private string _VGA { get; set; }
        public string VGA { get => _VGA; set { _VGA = value; OnPropertyChanged(); } }
        private string _Size { get; set; }
        public string Size { get => _Size; set { _Size = value; OnPropertyChanged(); } }
        private string _OS { get; set; }
        public string OS { get => _OS; set { _OS = value; OnPropertyChanged(); } }

        private string _Color { get; set; }
        public string Color { get => _Color; set { _Color = value; OnPropertyChanged(); } }

        private string _LI1 { get; set; }
        public string LI1 { get => _LI1; set { _LI1 = value; OnPropertyChanged(); } }

        private string _LI2 { get; set; }
        public string LI2 { get => _LI2; set { _LI2 = value; OnPropertyChanged(); } }
        private string _LI3 { get; set; }
        public string LI3 { get => _LI3; set { _LI3 = value; OnPropertyChanged(); } }
        private string _LI4 { get; set; }
        public string LI4 { get => _LI4; set { _LI4 = value; OnPropertyChanged(); } }

        private ObservableCollection<SANPHAM> _ProductList;
        public ObservableCollection<SANPHAM> ProductList { get => _ProductList; set { _ProductList = value; OnPropertyChanged(); } }

        //private ObservableCollection<CTSP> _ProductDetailList;
        //public ObservableCollection<CTSP> ProductDetailList { get => _ProductDetailList; set { _ProductDetailList = value; OnPropertyChanged(); } }




        public GoodDetailViewModel(NavigationStore navigationStore)
        {
            ProductList = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs);
            navEdit = new NavigationCommand<EditGoodViewModel>(navigationStore, () => new EditGoodViewModel(navigationStore));
            navBack = new NavigationCommand<GoodsViewModel>(navigationStore, () => new GoodsViewModel(navigationStore));
        }

    }
}
