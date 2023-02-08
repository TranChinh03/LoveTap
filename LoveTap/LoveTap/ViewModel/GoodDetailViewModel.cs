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
using System.Windows.Media.Imaging;

namespace LoveTap.ViewModel
{
    internal class GoodDetailViewModel:BaseViewModel
    {
        public ICommand navEdit { get; set; }
        public ICommand navBack { get; set; }
        public ICommand BackCmd { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand LoadedGoodDetail { get; set; }
        public ICommand ChangeImage1 { get; set; }
        public ICommand ChangeImage2 { get; set; }
        public ICommand ChangeImage3 { get; set; }
        public ICommand ChangeImage4 { get; set; }
        public ICommand LastImg { get; set; }
        public ICommand NextImg { get; set; }

        private int _ID { get; set; }
        public int ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }

        private string _Name { get; set; }
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }

        //private string _ProductID { get; set; }
        //public string ProductID { get => _ProductID; set { _ProductID = value; OnPropertyChanged(); } }

        private string _Madein { get; set; }
        public string Madein { get => _Madein; set { _Madein = value; OnPropertyChanged(); } }

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
        private string _Ava { get; set; }
        public string Ava { get => _Ava; set { _Ava = value; OnPropertyChanged(); } }
        private string _Ava1 { get; set; }
        public string Ava1 { get => _Ava1; set { _Ava1 = value; OnPropertyChanged(); } }
        private string _Ava2 { get; set; }
        public string Ava2 { get => _Ava2; set { _Ava2 = value; OnPropertyChanged(); } }
        private string _Ava3 { get; set; }
        public string Ava3 { get => _Ava3; set { _Ava3 = value; OnPropertyChanged(); } }
        private string _Ava4 { get; set; }
        public string Ava4 { get => _Ava4; set { _Ava4 = value; OnPropertyChanged(); } }

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

        private List<string> _MyBenefitList = new List<string>();
        public List<string> MyBenefitList { get => _MyBenefitList; set { _MyBenefitList = value; OnPropertyChanged(); } }

        
        public GoodDetailViewModel(NavigationStore navigationStore)
        {
            ProductList = new ObservableCollection<SANPHAM>(DataProvider.Ins.DB.SANPHAMs.Where(x => x.DELETED == false));
            ProductDetailList = new ObservableCollection<CTSP>(DataProvider.Ins.DB.CTSPs);
            BenefitList = new ObservableCollection<LOIICH>(DataProvider.Ins.DB.LOIICHes);
            LoadedGoodDetail = new RelayCommand<UserControl>((p) => true, (p) =>
            {
                GoodsViewModel.Product temp = GoodsViewModel.CurrentSelected;
                HomeViewModel.BestSelling temp2 = HomeViewModel.BestslSelected;
                
               
                if (HomeViewModel.flag != 1)
                {
                    foreach (SANPHAM sp in ProductList)
                    {
                        if (sp.TEN == temp.Ten)
                        {
                            ID = sp.MASP;
                            Name = sp.TEN;
                            Price = (double)sp.GIA;
                            Madein = sp.NUOCSX;
                        }    
                    }
                }
                else
                {
                    foreach (SANPHAM sp in ProductList)
                    {
                        if (sp.TEN == temp2.Ten)

                        {
                            ID = sp.MASP;
                            Name = sp.TEN;
                            Price = (double)sp.GIA;
                            Madein = sp.NUOCSX;
                        }
                    }
                }
               
                foreach (CTSP ctsp in ProductDetailList)
                {
                    if (ctsp.MASP == ID)
                    {
                        RAM = ctsp.RAM;
                        CPU = ctsp.CPU;
                        HD = ctsp.HD;
                        VGA = ctsp.VGA;
                        Size = ctsp.SCREENSIZE;
                        Color = ctsp.COLOR;
                        OS = ctsp.OS;
                        Ava1 = ctsp.AVA1;
                        Ava2 = ctsp.AVA2;
                        Ava3 = ctsp.AVA3;
                        Ava4 = ctsp.AVA4;
                        Ava = Ava1;
                    }
                }

                foreach (LOIICH li in BenefitList)
                {
                    if (li.MASP == ID)
                    {
                        LI1 = li.LI1;
                        MyBenefitList.Add(LI1);
                        LI2 = li.LI2;
                        MyBenefitList.Add(LI2);
                        LI3 = li.LI3;
                        MyBenefitList.Add(LI3);
                        LI4 = li.LI4;
                        MyBenefitList.Add(LI4);
                    }
                }

            });

            if (HomeViewModel.flag != 1)
                BackCmd = new NavigationCommand<GoodsViewModel>(navigationStore, () => new GoodsViewModel(navigationStore));
            else
                BackCmd = new NavigationCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
            navEdit = new NavigationCommand<EditGoodViewModel>(navigationStore, () => new EditGoodViewModel(navigationStore));
            navBack = new RelayCommand<GoodDetailViewModel>((p) => { return true; }, (p) => _Back(p));

            ChangeImage1 = new RelayCommand<Image>((p) => true, (p) => {
                Ava = Ava1;
                Uri fileUri = new Uri(Ava1, UriKind.RelativeOrAbsolute);
                p.Source = new BitmapImage(fileUri);
            });
            ChangeImage2 = new RelayCommand<Image>((p) => true, (p) => { 
                Ava = Ava2;
                Uri fileUri = new Uri(Ava2, UriKind.RelativeOrAbsolute);
                p.Source = new BitmapImage(fileUri);
            });
            ChangeImage3 = new RelayCommand<Image>((p) => true, (p) => { 
                Ava = Ava3;
                Uri fileUri = new Uri(Ava3, UriKind.RelativeOrAbsolute);
                p.Source = new BitmapImage(fileUri);
            });
            ChangeImage4 = new RelayCommand<Image>((p) => true, (p) => { 
                Ava = Ava4;
                Uri fileUri = new Uri(Ava4, UriKind.RelativeOrAbsolute);
                p.Source = new BitmapImage(fileUri);
            });
            LastImg = new RelayCommand<Image>((p) => true, (p) => {
                if (Ava == Ava1)
                {
                    Ava = Ava4;
                    Uri fileUri = new Uri(Ava4, UriKind.RelativeOrAbsolute);
                    p.Source = new BitmapImage(fileUri);
                    return;
                }
                if (Ava == Ava2)
                {
                    Ava = Ava1;
                    Uri fileUri = new Uri(Ava1, UriKind.RelativeOrAbsolute);
                    p.Source = new BitmapImage(fileUri);
                    return;
                }
                if (Ava == Ava3)
                {
                    Ava = Ava2;
                    Uri fileUri = new Uri(Ava2, UriKind.RelativeOrAbsolute);
                    p.Source = new BitmapImage(fileUri);
                    return;
                }
                if (Ava == Ava4)
                {
                    Ava = Ava3;
                    Uri fileUri = new Uri(Ava3, UriKind.RelativeOrAbsolute);
                    p.Source = new BitmapImage(fileUri);
                    return;
                }
            });
            NextImg = new RelayCommand<Image>((p) => true, (p) => {
                if (Ava == Ava1)
                {
                    Ava = Ava2;
                    Uri fileUri = new Uri(Ava2, UriKind.RelativeOrAbsolute);
                    p.Source = new BitmapImage(fileUri);
                    return;
                }
                if (Ava == Ava2)
                {
                    Ava = Ava3;
                    Uri fileUri = new Uri(Ava3, UriKind.RelativeOrAbsolute);
                    p.Source = new BitmapImage(fileUri);
                    return;
                }
                if (Ava == Ava3)
                {
                    Ava = Ava4;
                    Uri fileUri = new Uri(Ava4, UriKind.RelativeOrAbsolute);
                    p.Source = new BitmapImage(fileUri);
                    return;
                }
                if (Ava == Ava4)
                {
                    Ava = Ava1;
                    Uri fileUri = new Uri(Ava1, UriKind.RelativeOrAbsolute);
                    p.Source = new BitmapImage(fileUri);
                    return;
                }
            });
            DeleteCommand = new RelayCommand<UserControl>((p) => true, (p) =>
            {
                var ctsp = DataProvider.Ins.DB.CTSPs.Where(x => x.MASP == ID).SingleOrDefault();
                if (ctsp != null)
                {
                    ctsp.DELETED = true;
                }

                DataProvider.Ins.DB.SaveChanges();
                
                var li = DataProvider.Ins.DB.LOIICHes.Where(x => x.MASP == ID).SingleOrDefault();
                if(li != null)
                {
                    li.DELETED = true;
                }    
                DataProvider.Ins.DB.SaveChanges();

                var sp = DataProvider.Ins.DB.SANPHAMs.Where(x => x.MASP == ID).SingleOrDefault();
                sp.DELETED = true;
                DataProvider.Ins.DB.SaveChanges();
            });
        }

        void _Back(GoodDetailViewModel p)
        {
            
            HomeViewModel.flag = 0;
        }
    }
}
