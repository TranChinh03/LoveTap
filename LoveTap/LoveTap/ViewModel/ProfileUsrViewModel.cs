using LoveTap.Commands;
using LoveTap.Model;
using LoveTap.Stores;
using LoveTap.UserControlCustom;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
using System.IO;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace LoveTap.ViewModel
{
    public class ProfileUsrViewModel : BaseViewModel
    {
        public int UserID { get; set; }
        public ICommand NavChangePw { get; set; }
        public ICommand NavEditUsr { get; }

        private ObservableCollection<NHANVIEN> _User;
        public ObservableCollection<NHANVIEN> User { get => _User; set { _User = value; OnPropertyChanged(); } }
        private ObservableCollection<HOADON> _DeliveryList;
        public ObservableCollection<HOADON> DeliveryList { get => _DeliveryList; set { _DeliveryList = value; OnPropertyChanged(); } }
        private ObservableCollection<PHIEUNHAP> _ReceiveList;
        public ObservableCollection<PHIEUNHAP> ReceiveList { get => _ReceiveList; set { _ReceiveList = value; OnPropertyChanged(); } }
        public int flag = 0;

        private string _FullName;
        public string FullName { get => _FullName; set { _FullName = value; OnPropertyChanged(); } }
        private Nullable<System.DateTime> _DOB;
        public Nullable<System.DateTime> DOB { get => _DOB; set { _DOB = value; OnPropertyChanged(); } }

        private string _PhoneNumber;
        public string PhoneNumber { get => _PhoneNumber; set { _PhoneNumber = value; OnPropertyChanged(); } }

        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }

        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }

        private int _ID;
        public int ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }

        private int _Branch;
        public int Branch { get => _Branch; set { _Branch = value; OnPropertyChanged(); } }

        private string _CoefficientsSalary;
        public string CoefficientsSalary { get => _CoefficientsSalary; set { _CoefficientsSalary = value; OnPropertyChanged(); } }

        private string _BasicPay;
        public string BasicPay { get => _BasicPay; set { _BasicPay = value; OnPropertyChanged(); } }

        private string _Role;
        public string Role { get => _Role; set { _Role = value; OnPropertyChanged(); } }

        private string _Ava;
        public string Ava { get => _Ava; set { _Ava = value; OnPropertyChanged(); } }

        private string _NumberOfOrder;
        public string OrdersCount { get => _NumberOfOrder; set { _NumberOfOrder = value; OnPropertyChanged(); } }
        private string _BranchName;
        public string BranchName { get => _BranchName; set { _BranchName = value; OnPropertyChanged(); } }
        private string _BranchAdress;
        public string BranchAdress { get => _BranchAdress; set { _BranchAdress = value; OnPropertyChanged(); } }
        private string _localLink = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug"));
        private string _LinkAddImage;
        public string LinkAddImage { get => _LinkAddImage; set { _LinkAddImage = value; OnPropertyChanged(); } }

        string Name;
        public ICommand AddImage { get; set; }
        public ICommand ReloadAva { get; set; }

        public ProfileUsrViewModel(NavigationStore navigationStore)
        {
            UserID = MainViewModel.ID;
            User = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs.Where(x => x.MANV == UserID));

            DeliveryList = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
            ReceiveList = new ObservableCollection<PHIEUNHAP>(DataProvider.Ins.DB.PHIEUNHAPs);
            if (User != null && User.Count > 0)
            {
                FullName = User[0].HOTEN;
                DOB = User[0].NTNS;
                PhoneNumber = User[0].SDT;
                Email = User[0].EMAIL;
                Address = User[0].DIACHI;
                ID = User[0].MANV;
                Branch = (int)User[0].MACN;
                CoefficientsSalary = User[0].HESOLUONG.ToString();
                BasicPay = User[0].LUONGCB.ToString();
                string role = User[0].VAITRO.ToString();
                if (role == "True")
                    Role = "Admin";
                else
                    Role = "Staff";
                Ava = User[0].AVA;
                OrdersCount = DataProvider.Ins.DB.HOADONs.Where(x => x.MANV == UserID).Count().ToString();
                var UsrBranch = DataProvider.Ins.DB.CHINHANHs.Where(x => x.MACN == Branch).ToList();
                BranchName = UsrBranch[0].TENCN;
                BranchAdress = UsrBranch[0].DIACHI;
            };

            NavEditUsr = new NavigationCommand<UsrPro5EditViewModel>(navigationStore, () => new UsrPro5EditViewModel(navigationStore));
            NavChangePw = new NavigationCommand<CreatePwViewModel>(navigationStore, () => new CreatePwViewModel(navigationStore));

            LinkAddImage = "";
            AddImage = new RelayCommand<ImageBrush>((p) => true, (p) => _AddImage(p));
            ReloadAva = new RelayCommand<MainWd>((p) => true, (p) =>
            {
                Uri fileUri = new Uri(LinkAddImage);
                p.ava.ImageSource = new BitmapImage(fileUri);
            });
        }
        //void _AddImage(Image img)
        //{
        //    OpenFileDialog open = new OpenFileDialog();
        //    open.Filter = "Image Files(*.jpg; *.png)|*.jpg; *.png";
        //    if (open.ShowDialog() == true)
        //    {
        //        LinkAddImage = open.FileName;
        //    };
        //    if (LinkAddImage == "../img/person.jpg")
        //    {
        //        Uri fileUri = new Uri(LinkAddImage, UriKind.Relative);
        //        img.Source = new BitmapImage(fileUri);
        //    }
        //    else
        //    {
        //        Uri fileUri = new Uri(LinkAddImage);
        //        img.Source = new BitmapImage(fileUri);
        //    }
        //}
        void _AddImage(ImageBrush img)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.png)|*.jpg; *.png";

            if (open.ShowDialog() == true)
            {
                if (open.FileName != "")
                    LinkAddImage = open.FileName;
            };
            //Uri fileUri = new Uri(LinkAddImage);
            //img.ImageSource = new BitmapImage(fileUri);

            if (MainViewModel.flagAvt)
            {
                try
                {
                    File.Copy(LinkAddImage, _localLink + @"img\UserAvatar\" + "NV_" + User[0].MANV + ((LinkAddImage.Contains(".jpg")) ? ".jpg" : ".png").ToString(), true);
                    var image = new BitmapImage();
                    string str = _localLink + @"img\UserAvatar\" + "NV_" + User[0].MANV + ((LinkAddImage.Contains(".jpg")) ? ".jpg" : ".png").ToString();
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = new Uri(str);
                    image.EndInit();

                    img.ImageSource = image;
                    User[0].AVA =  @"..\img\UserAvatar\" + "NV_" + User[0].MANV + ((LinkAddImage.Contains(".jpg")) ? ".jpg" : ".png").ToString();

                    //User[0].AVA = image.ToString();
                    DataProvider.Ins.DB.SaveChanges();
                }
                catch
                {
                    File.Copy(LinkAddImage, _localLink + @"img\UserAvatar\" + "NV" + User[0].MANV + ((LinkAddImage.Contains(".jpg")) ? ".jpg" : ".png").ToString(), true);
                    var image = new BitmapImage();
                    string str = _localLink + @"img\UserAvatar\" + "NV" + User[0].MANV + ((LinkAddImage.Contains(".jpg")) ? ".jpg" : ".png").ToString();
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = new Uri(str);
                    image.EndInit();

                    img.ImageSource = image;
                    User[0].AVA = @"..\img\UserAvatar\" + "NV" + User[0].MANV + ((LinkAddImage.Contains(".jpg")) ? ".jpg" : ".png").ToString();

                    //User[0].AVA = image.ToString();
                    DataProvider.Ins.DB.SaveChanges();
                }
            }
            else {
                MainViewModel.flagAvt = false;
                try
                {
                    File.Copy(LinkAddImage, _localLink + @"img\UserAvatar\" + "NV" + User[0].MANV + ((LinkAddImage.Contains(".jpg")) ? ".jpg" : ".png").ToString(), true);
                    var image = new BitmapImage();
                    string str = _localLink + @"img\UserAvatar\" + "NV" + User[0].MANV + ((LinkAddImage.Contains(".jpg")) ? ".jpg" : ".png").ToString();
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = new Uri(str);
                    image.EndInit();

                    img.ImageSource = image;
                    User[0].AVA =  @"..\img\UserAvatar\" + "NV" + User[0].MANV + ((LinkAddImage.Contains(".jpg")) ? ".jpg" : ".png").ToString();

                    //User[0].AVA = image.ToString();
                    DataProvider.Ins.DB.SaveChanges();
                }
                catch
                {
                    File.Copy(LinkAddImage, _localLink + @"img\UserAvatar\" + "NV_" + User[0].MANV + ((LinkAddImage.Contains(".jpg")) ? ".jpg" : ".png").ToString(), true);
                    var image = new BitmapImage();
                    string str = _localLink + @"img\UserAvatar\" + "NV_" + User[0].MANV + ((LinkAddImage.Contains(".jpg")) ? ".jpg" : ".png").ToString();
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = new Uri(str);
                    image.EndInit();

                    img.ImageSource = image;
                    User[0].AVA =  @"..\img\UserAvatar\" + "NV_" + User[0].MANV + ((LinkAddImage.Contains(".jpg")) ? ".jpg" : ".png").ToString();

                    //User[0].AVA = image.ToString();
                    DataProvider.Ins.DB.SaveChanges();
                }
            }
        }
    }
}