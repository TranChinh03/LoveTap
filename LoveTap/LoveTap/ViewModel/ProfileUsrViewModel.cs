using LoveTap.UserControlCustom;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Xml.Linq;

namespace LoveTap.ViewModel
{
    internal class ProfileUsrViewModel : BaseViewModel
    {
        public ICommand GetIdTab { get; set; }

        public ICommand SwitchTab { get; set; }
        public ICommand changePw { get; set; }
        public ICommand HiddenBG { get; set; }

        string Name;
        public ProfileUsrViewModel()
        {
            GetIdTab = new RelayCommand<Button>((p) => true, (p) => Name = p.Uid);
            SwitchTab = new RelayCommand<HomePersonal>((p) => true, (p) => switchtab(p));
            changePw = new RelayCommand<CreatePasswordUC>((p) => true, (p) => changePass(p));
            HiddenBG = new RelayCommand<Grid>((p) => true, (p) => hiddenBG(p));
        }
        void switchtab(HomePersonal p)
        {
            int index = int.Parse(Name);
            switch (index)
            {
                case 1:
                    {
                        p.changePW.Visibility=Visibility.Visible;
                        break;
                    }
                case 2:
                    {
                        HomeProfileEdit hpEditWd = new HomeProfileEdit();
                        hpEditWd.ShowDialog();
                        break;
                    }

            }
        }

        void changePass(CreatePasswordUC p)
        {
            p.Visibility = Visibility.Collapsed;
        }

        void hiddenBG(Grid p)
        {
            p.Visibility= Visibility.Collapsed;
        }
    }
}
