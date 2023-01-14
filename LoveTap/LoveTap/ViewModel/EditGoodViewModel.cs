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
using LoveTap.Model;
using System.Collections.ObjectModel;
using System.Net;

namespace LoveTap.ViewModel
{
    internal class EditGoodViewModel:BaseViewModel
    {
        public ICommand navDone { get; set; }
        public ICommand navBack { get; set; }

        public ICommand LoadedEditGood { get; set; }
        public ICommand DoneCommand { get; set; } 

        private int _ID { get; set; }
        public int ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }

        private string _Name { get; set; }
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }

        //private string _ProductID { get; set; }
        //public string ProductID { get => _ProductID; set { _ProductID = value; OnPropertyChanged(); } }

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


        private ObservableCollection<CTSP> _ProductDetailList;
        public ObservableCollection<CTSP> ProductDetailList { get => _ProductDetailList; set { _ProductDetailList = value; OnPropertyChanged(); } }

        private ObservableCollection<LOIICH> _BenefitList;
        public ObservableCollection<LOIICH> BenefitList { get => _BenefitList; set { _BenefitList = value; OnPropertyChanged(); } }
        public EditGoodViewModel(NavigationStore navigationStore)
        {
            navDone = new NavigationCommand<GoodDetailViewModel>(navigationStore, () => new GoodDetailViewModel(navigationStore));
            navBack = new NavigationCommand<GoodDetailViewModel>(navigationStore, () => new GoodDetailViewModel(navigationStore));

            ProductList = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs.Where(x => x.DELETED == false));
            ProductDetailList = new ObservableCollection<CTSP>(DataProvider.Ins.DB.CTSPs);
            BenefitList = new ObservableCollection<LOIICH>(DataProvider.Ins.DB.LOIICHes);

            LoadedEditGood = new RelayCommand<UserControl>((p) => true, (p) =>
            {
                GoodsViewModel.Product temp = GoodsViewModel.CurrentSelected;
                foreach (SANPHAM sp in ProductList)
                {
                    if(sp.TEN == temp.Ten)
                    {
                        ID = sp.MASP;
                        Name = sp.TEN;
                        Price = (double)sp.GIA;
                    }
                }
                foreach (CTSP ctsp in ProductDetailList)
                {
                    if (ctsp.MASP == ID)
                    {
                        RAM = ctsp.RAM;
                        CPU = ctsp.CPU;
                        Color = ctsp.COLOR;
                        OS = ctsp.OS;
                        HD = ctsp.HD;
                        Size = ctsp.SCREENSIZE;
                        VGA = ctsp.VGA;
                    }
                }

                foreach (LOIICH li in BenefitList)
                {
                    if (li.MASP == ID)
                    {
                        LI1 = li.LI1;
                        LI2 = li.LI2;
                        LI3 = li.LI3;
                        LI4 = li.LI4;
                    }
                }
            });

            DoneCommand = new RelayCommand<object>((p) => {
                if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Price.ToString()))
                    return false;
                return true;
            }, (p) =>
            {
                var product = DataProvider.Ins.DB.SANPHAMs.Where(x => x.MASP == ID).SingleOrDefault();
                product.TEN = Name;
                product.GIA = Price;
                var productDetail = DataProvider.Ins.DB.CTSPs.Where(x => x.MASP == ID).SingleOrDefault();
                productDetail.RAM = RAM;
                productDetail.CPU = CPU;
                productDetail.OS = OS;
                productDetail.HD = HD;
                productDetail.SCREENSIZE = Size;
                productDetail.VGA = VGA;
                productDetail.COLOR = Color;
                var benefit = DataProvider.Ins.DB.LOIICHes.Where(x => x.MASP == ID).SingleOrDefault();
                benefit.LI1 = LI1;
                benefit.LI2 = LI2;
                benefit.LI3 = LI3;
                benefit.LI4 = LI4;
                DataProvider.Ins.DB.SaveChanges();
            });
        }

    }
}
