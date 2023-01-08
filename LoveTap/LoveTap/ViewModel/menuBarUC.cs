using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using LoveTap.Model;
using System.Security.Cryptography;


namespace LoveTap.ViewModel
{
    public class MenuBarUC : BaseViewModel
    {
        public string Name;
        public ICommand GetIdTab { get; set; }
        public ICommand SwitchTab { get; set; }
        public ICommand Loadwd { get; set; }
        public ICommand getMainWd { get; set; }



        public MenuBarUC()
        {
            // Loadwd = new RelayCommand<MainWindow>((p) => true, (p) => _Loadwd(p));
            GetIdTab = new RelayCommand<Button>((p) => true, (p) => Name = p.Uid);
            SwitchTab = new RelayCommand<object>((p) => true, (p) => switchtab(p));
            //getMainWd = new RelayCommand<object>((p) => true, (p) => getMain(p));


        }

        FrameworkElement GetWindowParent(Object u)
        {
            UserControl p = (u as UserControl);
            FrameworkElement parent = p;
            while (parent.Parent != null)
                parent = parent.Parent as FrameworkElement;
            return parent;
        }

        void switchtab(object p)
        {
            int index = int.Parse(Name);
            switch (index)
            {
                //            case 0:
                //                {
                //                    //_Loadwd(p);
                //                    p.Screen.NavigationService.Navigate(new HomeView());
                //                    break;
                //                }
                //            case 1:
                //                {
                //                    _Loadwd(p);
                //                    p.Main.NavigationService.Navigate(new OrderView());
                //                    break;
                //                }
                //            case 2:
                //                {
                //                    _Loadwd(p);
                //                    p.Main.NavigationService.Navigate(new ProductsView());
                //                    break;
                //                }
                case 3:
                    {
                        //_Loadwd(p);
                        //MainWindow mainwd = new MainWindow();
                        //mainwd.Main.NavigationService.Navigate(new CustomerWindow());
                        break;
                    }
                    //            case 4:
                    //                {
                    //                    _Loadwd(p);
                    //                    p.Main.NavigationService.Navigate(new ImportView());
                    //                    break;
                    //                }
                    //            case 5:
                    //                {
                    //                    _Loadwd(p);
                    //                    p.Main.NavigationService.Navigate(new ReportView());
                    //                    break;
                    //                }
                    //            case 6:
                    //                {
                    //                    _Loadwd(p);
                    //                    p.Main.NavigationService.Navigate(new QLNVView());
                    //                    break;
                    //                }
                    //            case 7:
                    //                {
                    //                    _Loadwd(p);
                    //                    p.Main.NavigationService.Navigate(new SettingView());
                    //                    break;
                    //                }
                    //            default:
                    //                {
                    //                    break;
                    //                }
                    //        }
            }


        }
    }
}
