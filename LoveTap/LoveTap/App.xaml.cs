using LoveTap.Stores;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using LoveTap.ViewModel;

namespace LoveTap
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationStore navigationStore = new NavigationStore();
            navigationStore.CurrentViewModel = new HomeViewModel(navigationStore);
            MainWd MainWd = new MainWd()
            {
                DataContext= new MainViewModel(navigationStore)
            };
            MainWindow.Show();
            base.OnStartup(e);

        }
    }
}
