using LoveTap.Model;
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

        public string TabName;
        public ICommand GetIdTab { get; set; }

        public ICommand SwitchTab { get; set; }

        public ICommand TenDangNhap_Loaded { get; set; }
        public ICommand Quyen_Loaded { get; set; }
        public ICommand LogOutCommand { get; set; }
        private Visibility _SetQuanLy;
        public Visibility SetQuanLy { get => _SetQuanLy; set { _SetQuanLy = value; OnPropertyChanged(); } }
        public string Name;
        private string _Ava;
        public string Ava { get => _Ava; set { _Ava = value; OnPropertyChanged(); } }
        public ICommand Loadwd { get; set; }
        public bool IsLoaded { get; set; } = false;
        public ICommand LoadedMainWd { get; set; }
        public static string ID { get; set; }
        public MainViewModel()
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
                    p.Show();
                }
                else
                {
                    p.Close();
                }
            });

            GetIdTab = new RelayCommand<Button>((p) => true, (p) => Name = p.Uid);
            SwitchTab = new RelayCommand<MainWindow>((p) => true, (p) => switchtab(p));

        }

        void switchtab(MainWindow p)
        {
            int index = int.Parse(Name);
            switch (index)
            {
                case 0:
                    {
                        p.Main.NavigationService.Navigate(new HomeScreen());
                        p.Tabtxbl.Text = "Home";
                        HomeCheck();
                        GoodsUncheck();
                        OrdersUncheck();
                        CustomerUncheck();
                        StatisticUncheck();
                        EmployeeUncheck();
                        break;
                    }
                case 1:
                    {
                        p.Tabtxbl.Text = "Goods";
                        HomeUnCheck();
                        GoodsCheck();
                        OrdersUncheck();
                        CustomerUncheck();
                        StatisticUncheck();
                        EmployeeUncheck();
                        p.Main.NavigationService.Navigate(new GoodsWindow());
                        break;
                    }
                case 2:
                    {
                        p.Tabtxbl.Text = "Orders";
                        HomeUnCheck();
                        GoodsUncheck();
                        OrdersCheck();
                        CustomerUncheck();
                        StatisticUncheck();
                        EmployeeUncheck();
                        p.Main.NavigationService.Navigate(new OrdersWindow());
                        break;
                    }
                case 3:
                    {
                        p.Tabtxbl.Text = "Customer";
                        HomeUnCheck();
                        GoodsUncheck();
                        OrdersUncheck();
                        CustomerCheck();
                        StatisticUncheck();
                        EmployeeUncheck();
                        p.Main.NavigationService.Navigate(new CustomerWindow());
                        break;
                    }
                case 4:
                    {
                        p.Tabtxbl.Text = "Statistic";
                        HomeUnCheck();
                        GoodsUncheck();
                        OrdersUncheck();
                        CustomerUncheck();
                        StatisticCheck();
                        EmployeeUncheck(); p.Main.NavigationService.Navigate(new SalesStatisticWindow());
                        break;
                    }
                case 5:
                    {
                        p.Tabtxbl.Text = "Employee";
                        HomeUnCheck();
                        GoodsUncheck();
                        OrdersUncheck();
                        CustomerUncheck();
                        StatisticUncheck();
                        EmployeeCheck();
                        p.Main.NavigationService.Navigate(new EmployeeWindow());
                        break;
                    }
                //case 6:
                //    {
                //        _Loadwd(p);
                //        p.Main.NavigationService.Navigate(new QLNVView());
                //        break;
                //    }
                case 7:
                    {
                        HomeUnCheck();
                        GoodsUncheck();
                        OrdersUncheck();
                        CustomerUncheck();
                        StatisticUncheck();
                        EmployeeUncheck();
                        p.Main.NavigationService.Navigate(new HomePersonal ());
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            void HomeCheck()
            {
                p.homeTbl.Foreground = Brushes.White;
                p.homeImg1.Visibility = Visibility.Collapsed;
                p.homeImg2.Visibility = Visibility.Visible;
            }

            void HomeUnCheck()
            {
                p.homeTbl.Foreground = new SolidColorBrush(Color.FromRgb(121, 167, 203));
                p.homeImg1.Visibility = Visibility.Visible;
                p.homeImg2.Visibility = Visibility.Collapsed;
            }

            void GoodsCheck()
            {
                p.goodsTbl.Foreground = Brushes.White;
                p.goodsImg1.Visibility = Visibility.Collapsed;
                p.goodsImg2.Visibility = Visibility.Visible;
            }

            void GoodsUncheck()
            {
                p.goodsTbl.Foreground = new SolidColorBrush(Color.FromRgb(121, 167, 203));
                p.goodsImg1.Visibility = Visibility.Visible;
                p.goodsImg2.Visibility = Visibility.Collapsed;
            }

            void OrdersCheck()
            {
                p.ordersTbl.Foreground = Brushes.White;
                p.ordersImg1.Visibility = Visibility.Collapsed;
                p.ordersImg2.Visibility = Visibility.Visible;
            }

            void OrdersUncheck()
            {
                p.ordersTbl.Foreground = new SolidColorBrush(Color.FromRgb(121, 167, 203));
                p.ordersImg1.Visibility = Visibility.Visible;
                p.ordersImg2.Visibility = Visibility.Collapsed;
            }
            void CustomerCheck()
            {
                p.customerTbl.Foreground = Brushes.White;
                p.customerImg1.Visibility = Visibility.Collapsed;
                p.customerImg2.Visibility = Visibility.Visible;
            }

            void CustomerUncheck()
            {
                p.customerTbl.Foreground = new SolidColorBrush(Color.FromRgb(121, 167, 203));
                p.customerImg1.Visibility = Visibility.Visible;
                p.customerImg2.Visibility = Visibility.Collapsed;
            }
            void StatisticCheck()
            {
                p.statisticTbl.Foreground = Brushes.White;
                p.statisticImg1.Visibility = Visibility.Collapsed;
                p.statisticImg2.Visibility = Visibility.Visible;
            }

            void StatisticUncheck()
            {
                p.statisticTbl.Foreground = new SolidColorBrush(Color.FromRgb(121, 167, 203));
                p.statisticImg1.Visibility = Visibility.Visible;
                p.statisticImg2.Visibility = Visibility.Collapsed;
            }

            void EmployeeCheck()
            {
                p.employeeTbl.Foreground = Brushes.White;
                p.employeeImg1.Visibility = Visibility.Collapsed;
                p.employeeImg2.Visibility = Visibility.Visible;
            }
            void EmployeeUncheck()
            {
                p.employeeTbl.Foreground = new SolidColorBrush(Color.FromRgb(121, 167, 203));
                p.employeeImg1.Visibility = Visibility.Visible;
                p.employeeImg2.Visibility = Visibility.Collapsed;
            }
        }

    }
}
