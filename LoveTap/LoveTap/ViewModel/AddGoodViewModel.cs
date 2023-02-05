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
using static LoveTap.ViewModel.OrderDetailViewModel;

namespace LoveTap.ViewModel
{
    public class AddGoodViewModel:BaseViewModel
    {

        public ICommand navDone { get; set; }
        public ICommand navBack { get; set; }

        public ICommand AddCommand { get; set; }


        private int _ID;
        public int ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }

        private string _Name;
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }

        private string _Madein;
        public string Madein { get => _Madein; set { _Madein = value; OnPropertyChanged(); } }

        private string _Category;
        public string Category { get => _Category; set { _Category = value; OnPropertyChanged(); } }

        private double _Price;
        public double Price { get => _Price; set { _Price = value; OnPropertyChanged(); } }

        private string _CPU;
        public string CPU { get => _CPU; set { _CPU = value; OnPropertyChanged(); } }

        private string _RAM;
        public string RAM { get => _RAM; set { _RAM = value; OnPropertyChanged(); } }

        private string _HD;
        public string HD { get => _HD; set { _HD = value; OnPropertyChanged(); } }

        private string _VGA;
        public string VGA { get => _VGA; set { _VGA = value; OnPropertyChanged(); } }

        private string _ScreenSize;
        public string ScreenSize { get => _ScreenSize; set { _ScreenSize = value; OnPropertyChanged(); } }

        private string _OS;
        public string OS { get => _OS; set { _OS = value; OnPropertyChanged(); } }

        private string _Color;
        public string Color { get => _Color; set { _Color = value; OnPropertyChanged(); } }

        private string _LI1;
        public string LI1 { get => _LI1; set { _LI1 = value; OnPropertyChanged(); } }

        private string _LI2;
        public string LI2 { get => _LI2; set { _LI2 = value; OnPropertyChanged(); } }

        private string _LI3;
        public string LI3 { get => _LI3; set { _LI3 = value; OnPropertyChanged(); } }

        private string _LI4;
        public string LI4 { get => _LI4; set { _LI4 = value; OnPropertyChanged(); } }


        public string[] CategoryNameList { get; set; } = new string[DataProvider.Ins.DB.DANHMUCs.Count()];


        private ObservableCollection<DANHMUC> _CategoryList;
        public ObservableCollection<DANHMUC> CategoryList { get => _CategoryList; set { _CategoryList = value; OnPropertyChanged(); } }

        private ObservableCollection<SANPHAM> _ProductList;
        public ObservableCollection<SANPHAM> ProductList { get => _ProductList; set { _ProductList = value; OnPropertyChanged(); } }

        private ObservableCollection<CTSP> _ProductDetailList;
        public ObservableCollection<CTSP> ProductDetailList { get => _ProductDetailList; set { _ProductDetailList = value; OnPropertyChanged(); } }


        public AddGoodViewModel(NavigationStore navigationStore)
        {
            navDone = new NavigationCommand<GoodsViewModel>(navigationStore, () => new GoodsViewModel(navigationStore));
            navBack = new NavigationCommand<GoodsViewModel>(navigationStore, () => new GoodsViewModel(navigationStore));

            CategoryList = new ObservableCollection<DANHMUC>(DataProvider.Ins.DB.DANHMUCs.Where(x => x.DELETED == false));
            ProductList = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs.Where(x => x.DELETED == false));
            ProductDetailList = new ObservableCollection<CTSP>(DataProvider.Ins.DB.CTSPs.Where(x => x.DELETED == false));

            for(int i = 0; i< CategoryList.Count(); i++)
            {
                CategoryNameList[i] = CategoryList[i].TENDM;
            }

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Madein) ||
                string.IsNullOrEmpty(Category) || string.IsNullOrEmpty(Price.ToString()))
                    return false;
                var good = DataProvider.Ins.DB.SANPHAMs.Where(x => x.MASP == ID);
                if (good == null || good.Count() != 0)
                    return false;
                return true;
            }, (p) =>
            {
                var sp = new SANPHAM();
                sp.TEN = Name;
                sp.GIA = Price;
                sp.NUOCSX = Madein;
                foreach(DANHMUC dm in CategoryList)
                {
                    if (dm.TENDM == Category)
                        sp.MADM = dm.MADM;
                }
                sp.DELETED = false;
                DataProvider.Ins.DB.SANPHAMs.Add(sp);
                DataProvider.Ins.DB.SaveChanges();

                var li = new LOIICH();
                li.MASP = sp.MASP;
                li.LI1 = LI1;
                li.LI2 = LI2;
                li.LI3 = LI3;
                li.LI4 = LI4;
                li.DELETED=false;
                DataProvider.Ins.DB.LOIICHes.Add(li);
                DataProvider.Ins.DB.SaveChanges();


                var ctsp = new CTSP();
                ctsp.MASP = sp.MASP;
                ctsp.RAM = RAM;
                ctsp.CPU = CPU;
                ctsp.VGA = VGA;
                ctsp.OS = OS;
                ctsp.SCREENSIZE = ScreenSize;
                ctsp.COLOR = Color;
                ctsp.HD = HD;
                ctsp.DELETED = false;
                DataProvider.Ins.DB.CTSPs.Add(ctsp);
                DataProvider.Ins.DB.SaveChanges();
            });

        }

    }
}
