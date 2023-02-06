using LoveTap.Commands;
using LoveTap.Model;
using LoveTap.Stores;
using LoveTap.UserControlCustom;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LoveTap.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public static bool flagAvt=true;
        private readonly NavigationStore _navigationStore;
        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
        
        private string _TabName;
        public string TabName { get => _TabName; set { _TabName = value; OnPropertyChanged(); } }
        private string _NameUsr;
        public string NameUsr { get => _NameUsr; set { _NameUsr = value; OnPropertyChanged(); } }
        private string _Position;
        public string Position { get => _Position; set { _Position = value; OnPropertyChanged(); } }

        private string _Ava;
        public string Ava { get => _Ava; set { _Ava = value; OnPropertyChanged(); } }

        public bool IsLoaded { get; set; } = false;
        public ICommand LoadedMainWd { get; set; }
        public ICommand LogOut { get; set; }
        public ICommand navProfile { get; }
        public ICommand updateTabName { get; }
        public ICommand updateTabNameRd { get; }
        public ICommand navHome { get; }
        public ICommand navGood { get; }
        public ICommand navDelivery { get; }
        public ICommand navReceive { get; }
        public ICommand navCustomer { get; }
        public ICommand navStatistic { get; }
        public ICommand navEmployee { get; }
        public ICommand navSupplier { get; }
        public static int ID { get; set; }
        public static bool IsAdmin { get; set; } = false;
        public MainViewModel(NavigationStore navigationStore)
        {
            TabName = "Home";
            LoadedMainWd = new RelayCommand<MainWd>((p) => { return true; }, (p) =>
            {
                IsLoaded = true;
                if (p == null)
                    return;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;
                if (loginVM.IsLogin)
                {
                    ID = loginVM.ID;
                    NHANVIEN temp = (DataProvider.Ins.DB.NHANVIENs.Where(x => x.MANV == ID).ToList())[0];
                    NameUsr = temp.HOTEN;
                    Ava = temp.AVA;
                    int count = 0, i = NameUsr.Length;
                    string nametmp;
                    while (count < 2)
                    {
                        i--;
                        if (NameUsr[i] == ' ')
                            count++;
                    }
                    nametmp = NameUsr.Substring(i, NameUsr.Length-i);

                    p.wishTxt.Text = "Have a good day,"+ nametmp +"!";

                    NameUsr = nametmp;
                    if (temp.VAITRO == true)
                    {
                        IsAdmin = true;
                        Position = "Admin";
                    }
                    else
                    {
                        IsAdmin = false;
                        Position = "Staff";
                    }
                    p.Show();
                }
                else
                {
                    p.Close();
                }

            }
            );

            updateTabName = new RelayCommand<MainWd>((p) => { return  true; }, (p) => _Update(p));
            updateTabNameRd = new RelayCommand<MainWd>((p) => { return  true; }, (p) => _Update2(p));
            navProfile = new NavigationCommand<ProfileUsrViewModel>(navigationStore, () => new ProfileUsrViewModel(navigationStore));
            navHome = new NavigationCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
            navGood = new NavigationCommand<GoodsViewModel>(navigationStore, () => new GoodsViewModel(navigationStore));
            navDelivery = new NavigationCommand<OrdersViewModel>(navigationStore, () => new OrdersViewModel(navigationStore));
            navReceive = new NavigationCommand<ReceiveOrderViewModel>(navigationStore, () => new ReceiveOrderViewModel(navigationStore));
            navCustomer = new NavigationCommand<CustomerViewModel>(navigationStore, () => new CustomerViewModel(navigationStore));
            navStatistic = new NavigationCommand<StatisticViewModel>(navigationStore, () => new StatisticViewModel(navigationStore));
            navEmployee = new NavigationCommand<EmployeeViewModel>(navigationStore, () => new EmployeeViewModel(navigationStore));
            navSupplier = new NavigationCommand<SupplierViewModel>(navigationStore, () => new SupplierViewModel(navigationStore));
            LogOut = new RelayCommand<MainWd>((p) => { return true; }, (p) =>
            {
                if (MessageBox.Show("Do you want to LogOut?", "Log Out", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    p.Hide();
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.ShowDialog();

                    if (loginWindow.DataContext == null)
                        return;
                    var loginVM = loginWindow.DataContext as LoginViewModel;
                    if (loginVM.IsLogin)
                    {
                        ID = loginVM.ID;
                        NHANVIEN temp = (DataProvider.Ins.DB.NHANVIENs.Where(x => x.MANV == ID).ToList())[0];
                        NameUsr = temp.HOTEN;
                        Ava = temp.AVA;
                        int count = 0, i = NameUsr.Length;
                        string nametmp;
                        while (count < 2)
                        {
                            i--;
                            if (NameUsr[i] == ' ')
                                count++;
                        }
                        nametmp = NameUsr.Substring(i, NameUsr.Length - i);

                        p.wishTxt.Text = "Have a good day," + nametmp + "!";

                        NameUsr = nametmp;
                        if (temp.VAITRO == true)
                        {
                            IsAdmin = true;
                            Position = "Admin";
                        }
                        else
                        {
                            IsAdmin = false;
                            Position = "Staff";
                        }
                        p.Show();
                    }
                    else
                    {
                        p.Close();
                    }
                }

            });

            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        void _Update(MainWd p)
        {
            p.Tabtxbl.Text = "Profile";
        }
        void _Update2(MainWd p)
        {
            if (p.Home.IsChecked== true) { p.Tabtxbl.Text = "Home"; }
            if (p.Goods.IsChecked== true) { p.Tabtxbl.Text = "Goods"; }
            if (p.Delivery.IsChecked== true) { p.Tabtxbl.Text = "Delivery"; }
            if (p.Receive.IsChecked== true) { p.Tabtxbl.Text = "Receive"; }
            if (p.Customer.IsChecked== true) { p.Tabtxbl.Text = "Customer"; }
            if (p.Statistic.IsChecked== true) { p.Tabtxbl.Text = "Statistic"; }
            if (p.Employee.IsChecked== true) { p.Tabtxbl.Text = "Employee"; }
            if (p.Supplier.IsChecked== true) { p.Tabtxbl.Text = "Supplier"; }
        }

        //M?y hàm này dùng ?? l?u ?nh, ??ng quan tâm nha
        //public byte[] getJPGFromImageControl(BitmapImage imageC)
        //{
        //    MemoryStream memStream = new MemoryStream();
        //    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
        //    encoder.Frames.Add(BitmapFrame.Create(imageC));
        //    encoder.Save(memStream);
        //    return memStream.ToArray();
        //}

        //getJPGFromImageControl(firmaUno.Source as BitmapImage);
    }
}
