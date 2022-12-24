using LoveTap.Commands;
using LoveTap.Model;
using LoveTap.Stores;
using LoveTap.UserControlCustom;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LoveTap.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
        public string TabName;

        public bool IsLoaded { get; set; } = false;
        public ICommand LoadedMainWd { get; set; }
        public ICommand navProfile { get;  }
        public ICommand navHome { get;  }
        public ICommand navGood { get;  }
        public ICommand navOrder { get;  }
        public ICommand navCustomer { get;  }
        public ICommand navStatistic { get;  }
        public ICommand navEmployee { get;  }
        public static string ID { get; set; }
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
                    if ((DataProvider.Ins.DB.NHANVIENs.Where(x => x.NVID == ID).ToList())[0].VAITRO == true)
                        IsAdmin = true;
                    p.Show();
                }
                else
                {
                    p.Close();
                }

            });

            navProfile = new NavigationCommand<ProfileUsrViewModel>(navigationStore, () => new ProfileUsrViewModel(navigationStore));
            navHome = new NavigationCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
            navGood = new NavigationCommand<GoodsViewModel>(navigationStore, () => new GoodsViewModel(navigationStore));
            navOrder = new NavigationCommand<OrdersViewModel>(navigationStore, () => new OrdersViewModel(navigationStore));
            navCustomer = new NavigationCommand<CustomerViewModel>(navigationStore, () => new CustomerViewModel(navigationStore));
            navStatistic = new NavigationCommand<StatisticViewModel>(navigationStore, () => new StatisticViewModel(navigationStore));
            navEmployee = new NavigationCommand<EmployeeViewModel>(navigationStore, () => new EmployeeViewModel(navigationStore));


            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
