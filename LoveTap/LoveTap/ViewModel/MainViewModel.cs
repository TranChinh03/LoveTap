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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LoveTap.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
        public string TabName;
        public string Name;
        public string VaiTro = "Staff";

        public bool IsLoaded { get; set; } = false;
        public ICommand LoadedMainWd { get; set; }
        public ICommand LogOut { get; set; }
        public ICommand navProfile { get;  }
        public ICommand navHome { get;  }
        public ICommand navGood { get;  }
        public ICommand navDelivery { get;  }
        public ICommand navReceive { get;  }
        public ICommand navCustomer { get;  }
        public ICommand navStatistic { get;  }
        public ICommand navEmployee { get;  }
        public static int ID { get; set; }
        public static bool IsAdmin { get; set; } = false;
        public MainViewModel(NavigationStore navigationStore)
        {
            LoadedMainWd = new RelayCommand<Window>((p) => { return true; }, (p) =>
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
                    Name = temp.HOTEN;
                    if (temp.VAITRO == true)
                    {
                        IsAdmin = true;
                        VaiTro = "Admin";
                    }
                    else
                    {
                        IsAdmin = false;
                        VaiTro = "Staff";
                    }
                    p.Show();
                }
                else
                {
                    p.Close();
                }

            }
            );

            navProfile = new NavigationCommand<ProfileUsrViewModel>(navigationStore, () => new ProfileUsrViewModel(navigationStore));
            navHome = new NavigationCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
            navGood = new NavigationCommand<GoodsViewModel>(navigationStore, () => new GoodsViewModel(navigationStore));
            navDelivery = new NavigationCommand<OrdersViewModel>(navigationStore, () => new OrdersViewModel(navigationStore));
            navReceive = new NavigationCommand<ReceiveOrderViewModel>(navigationStore, () => new ReceiveOrderViewModel(navigationStore));
            navCustomer = new NavigationCommand<CustomerViewModel>(navigationStore, () => new CustomerViewModel(navigationStore));
            navStatistic = new NavigationCommand<StatisticViewModel>(navigationStore, () => new StatisticViewModel(navigationStore));
            navEmployee = new NavigationCommand<EmployeeViewModel>(navigationStore, () => new EmployeeViewModel(navigationStore));
            LogOut = new RelayCommand<Window>((p) => { return true; }, (p) =>
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
                        Name = temp.HOTEN;
                        if (temp.VAITRO == true)
                        {
                            IsAdmin = true;
                            VaiTro = "Admin";
                        }
                        else
                        {
                            IsAdmin = false;
                            VaiTro = "Staff";
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
