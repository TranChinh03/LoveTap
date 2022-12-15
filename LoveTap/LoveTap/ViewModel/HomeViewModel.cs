using LoveTap.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace LoveTap.ViewModel
{
    internal class HomeViewModel
    {
        public ICommand GetIdButton { get; set; }

        public ICommand SwitchTab { get; set; }
        public ICommand Detail { get; set; }
        string Name;
        public HomeViewModel()
        {
            GetIdButton = new RelayCommand<Button>((p) => true, (p) => Name = p.Uid);
            SwitchTab = new RelayCommand<HomeScreen>((p) => true, (p) => switchtab(p));
            Detail = new RelayCommand<HomeScreen>((p) => { return p.BestSellingList.SelectedItem == null ? false : true; }, (p) => _DetailCs(p));

        }
        void switchtab(HomeScreen p)
        {
            int index = int.Parse(Name);
            switch (index)
            {
                case 1:
                    {
                        //p.AddCustomer.Visibility=Visibility.Visible;
                        break;
                    }
            }
        }

        void _DetailCs(HomeScreen p)
        {
           
            p.DetailBestSl.Visibility= Visibility.Visible;
            
        }

    }
}
